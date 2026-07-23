using Application.ProjetoCursosIdioma.Common;
using Application.ProjetoCursosIdioma.Dto.AlunoDtos;
using Application.ProjetoCursosIdioma.Dto.TurmaDtos;
using Application.ProjetoCursosIdioma.Interfaces;
using AutoMapper;
using Domain.ProjetoCursosIdioma.Entities;
using Domain.ProjetoCursosIdioma.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjetoCursosIdioma.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMapper _mapper;

        public TurmaService(ITurmaRepository turmaRepository, IMapper mapper)
        {
            this._mapper = mapper;
            this._turmaRepository = turmaRepository;
        }

        public async Task<Resultado<TurmaDto>> CreateAsync(TurmaAddRequestDto turmaAddRequestDto)
        {
            var existingNivel = await _turmaRepository.NivelTurmaExistAsync(turmaAddRequestDto.NivelTurmaId);
            if (!existingNivel)
            { return Resultado<TurmaDto>.Falha("Nível de turma não existe.", ErrorType.notFound); }

            var existingTurma = await _turmaRepository.TurmaExistAsync(turmaAddRequestDto.Name,
                                                                       turmaAddRequestDto.NivelTurmaId,
                                                                       turmaAddRequestDto.AnoLetivo,
                                                                       turmaAddRequestDto.NumeroTurma);
            if(existingTurma)
            { return Resultado<TurmaDto>.Falha("Turma com mesmo 'nome', 'nível', 'ano letivo' e 'número' já existe.", ErrorType.conflict); }

            var turma = _mapper.Map<Turma>(turmaAddRequestDto);
            turma = await _turmaRepository.CreateAsync(turma);

            return await ReloadTurmaAsync(turma.Id);
        }

        public async Task<List<TurmaDto>> GetAllAsync()
        {
            var turmas = await _turmaRepository.GetAllAsync();

            return _mapper.Map<List<TurmaDto>>(turmas);
        }

        public async Task<Resultado<TurmaDto>> GetByIdAsync(Guid Id)
        {
            var turma = await _turmaRepository.GetByIdAsync(Id);
            if (turma == null) { return Resultado<TurmaDto>.Falha("Turma não encontrada.", ErrorType.notFound); }

            return Resultado<TurmaDto>.Ok(_mapper.Map<TurmaDto>(turma));
        }

        public async Task<Resultado<TurmaDto>> UpdateAsync(Guid Id, TurmaUpdateRequestDto turmaUpdateRequestDto)
        {
            var existingTurma = await _turmaRepository.GetByIdAsync(Id);
            if (existingTurma == null)
            { return Resultado<TurmaDto>.Falha("Turma não encontrada.", ErrorType.notFound); }

            var existingNivel = await _turmaRepository.NivelTurmaExistAsync(turmaUpdateRequestDto.NivelTurmaId);
            if (!existingNivel)
            { return Resultado<TurmaDto>.Falha("Nível de turma não existe.", ErrorType.notFound); }
            
            var turmaDuplicated = await _turmaRepository.TurmaExistAsync(
                                                        turmaUpdateRequestDto.Name,
                                                        turmaUpdateRequestDto.NivelTurmaId,
                                                        turmaUpdateRequestDto.AnoLetivo,
                                                        turmaUpdateRequestDto.NumeroTurma,
                                                        Id);
            if (turmaDuplicated)
            { return Resultado<TurmaDto>.Falha("Outra turma com o mesmo nome, nível, ano letivo e número já existe.", ErrorType.conflict); }

            var turma = _mapper.Map<Turma>(turmaUpdateRequestDto);

            var updatedTurma = await _turmaRepository.UpdateAsync(Id, turma);
            if (updatedTurma == null)
            { return Resultado<TurmaDto>.Falha("Não foi possível atualizar a turma.", ErrorType.Interno); }

            return await ReloadTurmaAsync(Id);
        }

        public async Task<Resultado<TurmaDto>> DeleteAsync(Guid Id)
        {
            var turma = await _turmaRepository.GetByIdAsync(Id);
            if (turma == null)
            { return Resultado<TurmaDto>.Falha("Turma não encontrada.", ErrorType.notFound); }

            var alunosCount = await _turmaRepository.CountAlunosAsync(Id);
            if (alunosCount > 0)
            { return Resultado<TurmaDto>.Falha($"A turma não pode ser excluída porque possui {alunosCount} aluno(s) matriculado(s).", ErrorType.conflict); }

            var turmaDto = _mapper.Map<TurmaDto>(turma);

            var deletedTurma = await _turmaRepository.DeleteAsync(Id);

            if (deletedTurma == null)
            { return Resultado<TurmaDto>.Falha("Não foi possível excluir a turma.", ErrorType.Interno); }

            return Resultado<TurmaDto>.Ok(turmaDto);
        }

        private async Task<Resultado<TurmaDto>> ReloadTurmaAsync(Guid turmaId)
        {
            var updatedTurma = await _turmaRepository.GetByIdAsync(turmaId);
            if (updatedTurma == null)
            { return Resultado<TurmaDto>.Falha("A operação foi realizada, mas não foi possível recarregar os dados da turma.", ErrorType.Interno); }

            return Resultado<TurmaDto>.Ok(_mapper.Map<TurmaDto>(updatedTurma));
        }
    }
}
