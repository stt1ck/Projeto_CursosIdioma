using API.ProjetoCursosIdioma.Data;
using API.ProjetoCursosIdioma.Models.Domain;
using API.ProjetoCursosIdioma.Models.Dto.TurmaDto;
using API.ProjetoCursosIdioma.Repositories.TurmaRepFolder;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.ProjetoCursosIdioma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmasController : ControllerBase
    {
        //Injeções
        //Injeção e nomeação do DbContext
        private readonly PCI_DbContext _dbContext;
        private readonly ITurmaRepository turmaRepository;
        private readonly IMapper mapper;
        public TurmasController(PCI_DbContext dbContext, ITurmaRepository turmaRepository, IMapper mapper)
        {
            this._dbContext = dbContext;
            this.turmaRepository = turmaRepository;
            this.mapper = mapper;
        }

        //GET ALL Turmas
        //GET: /api/turmas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var turmasDomain = await turmaRepository.GetAllAsync();

            return Ok(mapper.Map<List<TurmaDto>>(turmasDomain));
        }

        // GET BY ID Turmas
        // GET: /api/turmas/{id}

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var turmaDomain = await turmaRepository.GetByIdAsync(Id);

            if (turmaDomain == null) { return NotFound(); }

            return Ok(mapper.Map<TurmaDto>(turmaDomain));
        }

        // POST Turmas
        // POST: /api/turmas
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TurmaAddRequestDto turmaAddRequestDto)
        {
            if (ModelState.IsValid)
            {
                var existingNivel = await turmaRepository.NivelTurmaExistAsync(turmaAddRequestDto.NivelTurmaId);
                if (existingNivel == false) { return NotFound("Nível não existe"); }

                var existingTurma = await turmaRepository.TurmaExistAsync(turmaAddRequestDto.Name, turmaAddRequestDto.NivelTurmaId, turmaAddRequestDto.AnoLetivo, turmaAddRequestDto.NumeroTurma);
                if (existingTurma == true) { return BadRequest("Turma com mesmo 'nome', 'nível', 'ano letivo' e 'número' já existe"); }

                var turmaDomain = mapper.Map<Turma>(turmaAddRequestDto);

                await turmaRepository.CreateAsync(turmaDomain);
                var createdTurma = await turmaRepository.GetByIdAsync(turmaDomain.Id);

                var turmaDto = mapper.Map<TurmaDto>(createdTurma);

                return CreatedAtAction(nameof(GetById), new { id = turmaDomain.Id }, turmaDto);
            }
            else { return BadRequest(ModelState); }
            
            
        }

        // UPDATE Turmas
        // PUT: /api/turmas/{Id}
        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] TurmaUpdateRequestDto turmaUpdateRequestDto)
        {
            var existingTurma = await turmaRepository.GetByIdAsync(Id);
            if (existingTurma == null) { return NotFound("Turma não encontrada."); }

            var existingNivel = await turmaRepository.NivelTurmaExistAsync(turmaUpdateRequestDto.NivelTurmaId);
            if (!existingNivel) { return NotFound("Nível não existe."); }

            var turmaDuplicated = await turmaRepository.TurmaExistAsync(
                    turmaUpdateRequestDto.Name,
                    turmaUpdateRequestDto.NivelTurmaId,
                    turmaUpdateRequestDto.AnoLetivo,
                    turmaUpdateRequestDto.NumeroTurma,
                    Id);
            if (turmaDuplicated) { return Conflict("Outra turma com o mesmo nome, nível, ano letivo e número já existe."); }

            var turmaDomain = mapper.Map<Turma>(turmaUpdateRequestDto);

            var updatedTurma = await turmaRepository.UpdateAsync(Id, turmaDomain);
            if (updatedTurma == null) { return NotFound("Turma não encontrada."); }

            var reloadTurma = await turmaRepository.GetByIdAsync(Id);
            if (reloadTurma == null) { return NotFound("Turma não encontrada."); }

            return Ok(mapper.Map<TurmaDto>(reloadTurma));
        }

        // DELETE Turmas
        // DELETE:/api/turmas/{Id}
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var existingTurma = await turmaRepository.GetByIdAsync(Id);

            if (existingTurma == null) { return NotFound("Turma não encontrada."); }

            var alunosCount = await turmaRepository.CountAlunosAsync(Id);

            if (alunosCount > 0)
            { return Conflict($"A turma não pode ser excluída porque possui {alunosCount} aluno(s) matriculado(s)."); }

            await turmaRepository.DeleteAsync(Id);

            return Ok(mapper.Map<TurmaDto>(existingTurma));
        }
    }
}
