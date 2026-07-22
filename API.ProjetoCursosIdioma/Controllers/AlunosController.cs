using API.ProjetoCursosIdioma.Data;
using API.ProjetoCursosIdioma.Models.Domain;
using API.ProjetoCursosIdioma.Models.Dto.AlunoDto_s;
using API.ProjetoCursosIdioma.Repositories.AlunoRepFolder;
using API.ProjetoCursosIdioma.Repositories.TurmaRepFolder;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API.ProjetoCursosIdioma.Validations;

namespace API.ProjetoCursosIdioma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        //Injeções
        //Injeção e nomeação do DbContext
        private readonly IAlunoRepository alunoRepository;
        private readonly IMapper mapper;
        private readonly ITurmaRepository turmaRepository;
        public AlunosController(IAlunoRepository alunoRepository, IMapper mapper, ITurmaRepository turmaRepository)
        {
            this.alunoRepository = alunoRepository;
            this.mapper = mapper;
            this.turmaRepository = turmaRepository;
        }

        //GET ALL Alunos
        //GET: /api/alunos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var alunosDomain = await alunoRepository.GetAllAsync();

            return Ok(mapper.Map<List<AlunoDto>>(alunosDomain));
        }

        // GET BY ID Alunos
        // GET: /api/alunos/{id}

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var alunoDomain = await alunoRepository.GetByIdAsync(Id);

            if (alunoDomain == null) {  return NotFound();  }

            return Ok(mapper.Map<AlunoDto>(alunoDomain));
        }

        // POST Alunos
        // POST: /api/alunos
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AlunoAddRequestDto alunoAddRequestDto)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }

            var cpfNormalized = NormalizadorCPF.Normalize(alunoAddRequestDto.Cpf);

            var alreadyUsedEmail = await alunoRepository.EmailAlreadyUsedAsync(alunoAddRequestDto.Email);
            if (alreadyUsedEmail) { return BadRequest("E-mail já cadastrado."); }

            var alreadyUsedCpf = await alunoRepository.CPFAlreadyUsedAsync(cpfNormalized);
            if (alreadyUsedCpf) { return BadRequest("CPF já cadastrado."); }

            var turmaIdsSemRepeticao = alunoAddRequestDto.TurmaIds.Distinct().ToList();
            if (turmaIdsSemRepeticao.Count != alunoAddRequestDto.TurmaIds.Count) { return BadRequest("O aluno não pode ser matriculado mais de uma vez na mesma turma."); }

            var turmasEncontradas = await turmaRepository.GetByIdsAsync(turmaIdsSemRepeticao);
            if (turmasEncontradas.Count != turmaIdsSemRepeticao.Count) { return BadRequest("Uma ou mais turmas informadas não existem."); }

            foreach (var turmaId in turmaIdsSemRepeticao)
            {
                var quantidadeAlunos = await turmaRepository.CountAlunosAsync(turmaId);
                if (quantidadeAlunos >= 5) { return BadRequest($"A turma de ID {turmaId} já possui o limite de 5 alunos."); }
            }
            var alunoDomain = mapper.Map<Aluno>(alunoAddRequestDto);
            alunoDomain.Cpf = cpfNormalized;

            foreach (var turmaId in turmaIdsSemRepeticao)
            {
                var alunoTurma = new AlunoTurma { TurmaId = turmaId };
                alunoDomain.AlunoTurmas.Add(alunoTurma);
            }

            await alunoRepository.CreateAsync(alunoDomain);
            var createdAluno = await alunoRepository.GetByIdAsync(alunoDomain.Id);

            var alunoDto = mapper.Map<AlunoDto>(createdAluno);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdAluno.Id },
                alunoDto);
        }

        [HttpPost]
        [Route("{alunoId:Guid}/turmas/{turmaId:Guid}")]
        public async Task<IActionResult> AddTurma([FromRoute] Guid alunoId, Guid turmaId)
        {
            var existingAluno = await alunoRepository.GetByIdAsync(alunoId);
            if (existingAluno == null) { return NotFound("Aluno não encontrado"); }

            var existingTurma = await turmaRepository.GetByIdAsync(turmaId);
            if (existingTurma == null) { return NotFound("Turma não existe"); }

            var alreadyInTurma = existingAluno.AlunoTurmas.Any(at => at.TurmaId == turmaId);
            if (alreadyInTurma) { return Conflict("O aluno já está matriculado nessa turma"); }

            var alunoCount = await turmaRepository.CountAlunosAsync(turmaId);
            if (alunoCount >= 5) { return Conflict("A turma já possui o limite máximo de 5 alunos"); }

            var alunoTurma = await alunoRepository.AddTurmaAsync(alunoId, turmaId);
            if (!alunoTurma) { return Conflict("Não foi possível realizar a matrícula porque ela já existe"); }

            var updatedAluno = await alunoRepository.GetByIdAsync(alunoId);

            return Ok(mapper.Map<AlunoDto>(updatedAluno));
        }

        // UPDATE Alunos
        // PUT: /api/alunos/{Id}
        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] AlunoUpdateRequestDto alunoUpdateRequestDto)
        {
            if(!ModelState.IsValid) {  return BadRequest(ModelState); }

            var existingAluno = await alunoRepository.GetByIdAsync(Id);
            if (existingAluno == null) { return NotFound("Aluno não encontrado."); }

            var emailAlreadyUsed = await alunoRepository.EmailAlreadyUsedByAnotherAlunoAsync(alunoUpdateRequestDto.Email, Id);
            if (emailAlreadyUsed) { return Conflict("E-mail já cadastrado por outro aluno."); }

            var alunoDomain = mapper.Map<Aluno>(alunoUpdateRequestDto);

            var updatedAluno = await alunoRepository.UpdateAsync(Id, alunoDomain);
            if (updatedAluno == null) { return NotFound("Aluno não encontrado.");  }

            return Ok(mapper.Map<AlunoDto>(updatedAluno));
        }

        // DELETE Alunos
        // DELETE:/api/alunos/{Id}
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var existingAluno = await alunoRepository.GetByIdAsync(Id);

            if (existingAluno == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            if (existingAluno.AlunoTurmas.Any())
            {
                return Conflict(
                    "O aluno não pode ser excluído porque está associado a uma ou mais turmas."
                );
            }

            await alunoRepository.DeleteAsync(Id);

            return Ok(mapper.Map<AlunoDto>(existingAluno));
        }

        [HttpDelete]
        [Route("{alunoId:Guid}/turmas/{turmaId:Guid}")]
        public async Task<IActionResult> DeleteTurma([FromRoute] Guid alunoId, [FromRoute] Guid turmaId)
        {
            var existingAluno = await alunoRepository.GetByIdAsync(alunoId);
            if (existingAluno == null) { return NotFound("Aluno não encontrado"); }

            var removeTurma = await  alunoRepository.DeleteTurmaAsync(alunoId, turmaId);
            if (!removeTurma)
            {
                return NotFound("O aluno não está matriculado na turma informada"); 
            }

            var updatedAluno = await alunoRepository.GetByIdAsync(alunoId);

            return Ok(mapper.Map<AlunoDto>(updatedAluno));
        }

    }
}
