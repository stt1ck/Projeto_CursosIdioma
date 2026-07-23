using Application.ProjetoCursosIdioma.Common;
using Application.ProjetoCursosIdioma.Dto.AlunoDtos;
using Application.ProjetoCursosIdioma.Interfaces;
using Application.ProjetoCursosIdioma.Validations;
using AutoMapper;
using Domain.ProjetoCursosIdioma.Entities;
using Domain.ProjetoCursosIdioma.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjetoCursosIdioma.Services;

public class AlunoService : IAlunoService
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly ITurmaRepository _turmaRepository;
    private readonly IMapper _mapper;

    public AlunoService(IAlunoRepository alunoRepository, ITurmaRepository turmaRepository, IMapper mapper)
    {
        this._alunoRepository = alunoRepository;
        this._turmaRepository = turmaRepository;
        this._mapper = mapper;
    }

    public async Task<List<AlunoDto>> GetAllAsync()
    {
        var alunos = await _alunoRepository.GetAllAsync();

        return _mapper.Map<List<AlunoDto>>(alunos);
    }

    public async Task<Resultado<AlunoDto>> GetByIdAsync(Guid Id)
    {
        var aluno = await _alunoRepository.GetByIdAsync(Id);
        if (aluno == null) { return Resultado<AlunoDto>.Falha( "Aluno não encontrado.", ErrorType.notFound); }

        return Resultado<AlunoDto>.Ok(_mapper.Map<AlunoDto>(aluno));
    }

    public async Task<Resultado<AlunoDto>> CreateAsync(AlunoAddRequestDto alunoAddRequestDto)
    {
        var normalizedCpf = NormalizadorCPF.Normalize(alunoAddRequestDto.Cpf);
        var normalizedEmail = alunoAddRequestDto.Email.Trim();
        var alreadySignedEmail = await _alunoRepository.EmailAlreadyUsedAsync(normalizedEmail);

        if (alreadySignedEmail)
        { return Resultado<AlunoDto>.Falha("E-mail já cadastrado.", ErrorType.conflict); }

        var alreadySignedCpf = await _alunoRepository.CPFAlreadyUsedAsync(normalizedCpf);
        if (alreadySignedCpf)
        { return Resultado<AlunoDto>.Falha("CPF já cadastrado.", ErrorType.conflict); }

        var idInfo = alunoAddRequestDto.TurmaIds ?? new List<Guid>();
        if (idInfo.Count == 0)
        { return Resultado<AlunoDto>.Falha("O aluno deve ser matriculado em pelo menos uma turma.", ErrorType.validation); }

        var turmaIds = idInfo.Distinct().ToList();
        if (turmaIds.Count != idInfo.Count)
        { return Resultado<AlunoDto>.Falha("A mesma turma foi informada mais de uma vez.", ErrorType.validation); }

        var turmas = await _turmaRepository.GetByIdsAsync(turmaIds);
        if (turmas.Count != turmaIds.Count)
        { return Resultado<AlunoDto>.Falha("Uma ou mais turmas informadas não existem.", ErrorType.notFound); }

        foreach (var turma in turmas)
        {
            var alunoCount = await _turmaRepository.CountAlunosAsync(turma.Id);
            if (!turma.HasAvailableSpace(alunoCount))
            { return Resultado<AlunoDto>.Falha($"A turma {turma.NumeroTurma} já possui o limite máximo de alunos.", ErrorType.conflict); }
        }

        var aluno = new Aluno
        {
            Nome = alunoAddRequestDto.Nome.Trim(),
            Cpf = normalizedCpf,
            Email = normalizedEmail
        };
        foreach (var turmaId in turmaIds)
        { aluno.AlunoTurmas.Add(new AlunoTurma { TurmaId = turmaId }); }

        await _alunoRepository.CreateAsync(aluno);
        return await ReloadAlunoAsync(aluno.Id);
    }

    public async Task<Resultado<AlunoDto>> AddTurmaAsync(Guid alunoId, Guid turmaId)
    {
        var aluno = await _alunoRepository.GetByIdAsync(alunoId);
        if (aluno == null)
        { return Resultado<AlunoDto>.Falha("Aluno não encontrado.", ErrorType.notFound); }

        var turma = await _turmaRepository.GetByIdAsync(turmaId);
        if (turma == null)
        { return Resultado<AlunoDto>.Falha("Turma não encontrada.", ErrorType.notFound); }

        if (aluno.AlreadySignedIn(turmaId))
        { return Resultado<AlunoDto>.Falha("O aluno já está matriculado nessa turma.", ErrorType.conflict); }

        var alunoCount = await _turmaRepository.CountAlunosAsync(turmaId);
        if (!turma.HasAvailableSpace(alunoCount))
        { return Resultado<AlunoDto>.Falha("A turma já possui o limite máximo de 5 alunos.", ErrorType.conflict); }

        var matriculaCriada = await _alunoRepository.AddTurmaAsync(alunoId, turmaId);
        if (!matriculaCriada)
        { return Resultado<AlunoDto>.Falha("Não foi possível realizar a matrícula.", ErrorType.conflict); }

        return await ReloadAlunoAsync(alunoId);
    }

    public async Task<Resultado<AlunoDto>> UpdateAsync(Guid Id, AlunoUpdateRequestDto alunoUpdateRequestDto)
    {
        var existingAluno = await _alunoRepository.GetByIdAsync(Id);
        if (existingAluno == null)
        { return Resultado<AlunoDto>.Falha("Aluno não encontrado.", ErrorType.notFound); }

        var normalizedEmail = alunoUpdateRequestDto.Email.Trim();

        var emailAlreadyTaken = await _alunoRepository.EmailAlreadyUsedAsync(normalizedEmail, Id);
        if (emailAlreadyTaken)
        { return Resultado<AlunoDto>.Falha("O e-mail informado já pertence a outro aluno.", ErrorType.conflict); }

        var newData = new Aluno
        {
            Nome = alunoUpdateRequestDto.Nome.Trim(),
            Email = normalizedEmail
        };

        var updatedAluno = await _alunoRepository.UpdateAsync(Id, newData);
        if (updatedAluno == null)
        { return Resultado<AlunoDto>.Falha("Não foi possível atualizar o aluno.", ErrorType.Interno); }

        return await ReloadAlunoAsync(Id);
    }

    public async Task<Resultado<AlunoDto>> DeleteAsync(Guid Id)
    {
        var aluno = await _alunoRepository.GetByIdAsync(Id);
        if (aluno == null)
        { return Resultado<AlunoDto>.Falha("Aluno não encontrado.", ErrorType.notFound); }

        if (aluno.IsSignedIn())
        { return Resultado<AlunoDto>.Falha("O aluno não pode ser excluído porque está associado a uma ou mais turmas.", ErrorType.conflict); }

        var alunoDto = _mapper.Map<AlunoDto>(aluno);

        await _alunoRepository.DeleteAsync(Id);

        return Resultado<AlunoDto>.Ok(alunoDto);
    }

    public async Task<Resultado<AlunoDto>> RemoveTurmaAsync(Guid alunoId, Guid turmaId)
    {
        var aluno = await _alunoRepository.GetByIdAsync(alunoId);
        if (aluno == null)
        { return Resultado<AlunoDto>.Falha("Aluno não encontrado.", ErrorType.notFound); }

        if (!aluno.AlreadySignedIn(turmaId))
        { return Resultado<AlunoDto>.Falha("O aluno não está matriculado na turma informada.", ErrorType.notFound); }

        var matriculaRemovida = await _alunoRepository.DeleteTurmaAsync(alunoId, turmaId);
        if (!matriculaRemovida)
        { return Resultado<AlunoDto>.Falha("Não foi possível remover a matrícula.", ErrorType.Interno); }

        return await ReloadAlunoAsync(alunoId);
    }

    private async Task<Resultado<AlunoDto>>ReloadAlunoAsync(Guid alunoId)
    {
        var updatedAluno = await _alunoRepository.GetByIdAsync(alunoId);
        if (updatedAluno == null)
        { return Resultado<AlunoDto>.Falha("A operação foi realizada, mas não foi possível recarregar os dados do aluno.", ErrorType.Interno); }

        return Resultado<AlunoDto>.Ok(_mapper.Map<AlunoDto>(updatedAluno));
    }
}