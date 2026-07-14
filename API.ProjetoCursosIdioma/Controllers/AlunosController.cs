using API.ProjetoCursosIdioma.Data;
using API.ProjetoCursosIdioma.Models.Domain;
using API.ProjetoCursosIdioma.Models.Dto.AlunoDto_s;
using API.ProjetoCursosIdioma.Repositories.AlunoRepFolder;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.ProjetoCursosIdioma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        //Injeções
        //Injeção e nomeação do DbContext
        private readonly PCI_DbContext _dbContext;
        private readonly IAlunoRepository alunoRepository;
        private readonly IMapper mapper;
        public AlunosController(PCI_DbContext dbContext, IAlunoRepository alunoRepository, IMapper mapper)
        {
            this._dbContext = dbContext;
            this.alunoRepository = alunoRepository;
            this.mapper = mapper;
        }

        //GET ALL Alunos
        //GET: /api/alunos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var alunosDomain = await alunoRepository.GetAllAsync();

            return Ok(mapper.Map<List<AlunoDto>>(alunosDomain));//preencher Dto
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
            if (ModelState.IsValid)
            {
                var alreadyUsedEmail = await alunoRepository.EmailAlreadyUsedAsync(alunoAddRequestDto.Email);
                if (alreadyUsedEmail == true) { return BadRequest("E-mail já cadastrado"); }

                var alreadyUsedCpf = await alunoRepository.CPFAlreadyUsedAsync(alunoAddRequestDto.Cpf);
                if (alreadyUsedCpf == true) { return BadRequest("Cpf já cadastrado"); }

                var alunoDomain = mapper.Map<Aluno>(alunoAddRequestDto);

                alunoDomain = await alunoRepository.CreateAsync(alunoDomain);

                var alunoDto = mapper.Map<AlunoDto>(alunoDomain);

                return CreatedAtAction(nameof(GetById), new { id = alunoDomain.Id }, alunoDto);
            }
            else { return BadRequest(ModelState); }
        }

        // UPDATE Alunos
        // PUT: /api/alunos/{Id}
        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] AlunoUpdateRequestDto alunoUpdateRequestDto)
        {
            if(ModelState.IsValid)
            {
                var alunoDomain = mapper.Map<Aluno>(alunoUpdateRequestDto);

                alunoDomain = await alunoRepository.UpdateAsync(Id, alunoDomain);
                if (alunoDomain == null) { return NotFound(); }

                var alunoDto = mapper.Map<AlunoDto>(alunoDomain);

                return Ok(alunoDto);
            }
            else { return BadRequest(ModelState); }
        }

        // DELETE Alunos
        // DELETE:/api/alunos/{Id}
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var alunoDomain = await alunoRepository.DeleteAsync(Id);
            if (alunoDomain == null) {  return NotFound();  }

            return Ok(mapper.Map<AlunoDto>(alunoDomain));
        }

    }
}
