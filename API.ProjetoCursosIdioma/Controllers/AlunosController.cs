using Application.ProjetoCursosIdioma.Common;
using Application.ProjetoCursosIdioma.Dto.AlunoDtos;
using Application.ProjetoCursosIdioma.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.ProjetoCursosIdioma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        //Injeções
        //Injeção e nomeação do Service
        private readonly IAlunoService alunoService;
        public AlunosController(IAlunoService alunoService)
        {
            this.alunoService = alunoService;
        }

        // Métodos
        //Conversão erros Service para Https
        private IActionResult ErrorMapping<T>(Resultado<T> resultado)
        {
            return resultado.ErrorType switch
            {
                ErrorType.notFound => NotFound(resultado.Mensagem),

                ErrorType.conflict => Conflict(resultado.Mensagem),

                ErrorType.validation => BadRequest(resultado.Mensagem),

                ErrorType.Interno => StatusCode(StatusCodes.Status500InternalServerError, resultado.Mensagem),

                _ => StatusCode(StatusCodes.Status500InternalServerError, "Erro inesperado.")
            };
        }

        //GET ALL Alunos
        //GET: /api/alunos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var alunos = await alunoService.GetAllAsync();

            return Ok(alunos);
        }

        // GET BY ID Alunos
        // GET: /api/alunos/{id}

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var resultado = await alunoService.GetByIdAsync(Id);
            if (!resultado.Success) { return ErrorMapping(resultado); }

            return Ok(resultado.Dados);
        }

        // POST Alunos
        // POST: /api/alunos
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AlunoAddRequestDto alunoAddRequestDto)
        {
            var resultado = await alunoService.CreateAsync(alunoAddRequestDto);
            if (!resultado.Success) 
            { return ErrorMapping(resultado); }

            return CreatedAtAction(nameof(GetById), new {id = resultado.Dados!.Id}, resultado.Dados);
        }

        [HttpPost]
        [Route("{alunoId:Guid}/turmas/{turmaId:Guid}")]
        public async Task<IActionResult> AddTurma([FromRoute] Guid alunoId, Guid turmaId)
        {
            var resultado = await alunoService.AddTurmaAsync(alunoId, turmaId);
            if (!resultado.Success)
            { return ErrorMapping(resultado); }

            return Ok(resultado.Dados);
        }

        // UPDATE Alunos
        // PUT: /api/alunos/{Id}
        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] AlunoUpdateRequestDto alunoUpdateRequestDto)
        {
            var resultado = await alunoService.UpdateAsync(Id, alunoUpdateRequestDto);
            if (!resultado.Success)
            { return ErrorMapping(resultado); }

            return Ok(resultado.Dados);
        }

        // DELETE Alunos
        // DELETE:/api/alunos/{Id}
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var resultado = await alunoService.DeleteAsync(Id);
            if (!resultado.Success)
            { return ErrorMapping(resultado); }

            return NoContent();
        }

        [HttpDelete]
        [Route("{alunoId:Guid}/turmas/{turmaId:Guid}")]
        public async Task<IActionResult> DeleteTurma([FromRoute] Guid alunoId, [FromRoute] Guid turmaId)
        {
            var resultado = await alunoService.RemoveTurmaAsync(alunoId, turmaId);
            if (!resultado.Success)
            { return ErrorMapping(resultado); }

            return Ok(resultado.Dados);
        }

    }
}
