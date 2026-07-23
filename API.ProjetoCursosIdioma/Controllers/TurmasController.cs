using Application.ProjetoCursosIdioma.Common;
using Application.ProjetoCursosIdioma.Dto.TurmaDtos;
using Application.ProjetoCursosIdioma.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace API.ProjetoCursosIdioma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmasController : ControllerBase
    {
        //Injeções
        //Injeção e nomeação do Service
        private readonly ITurmaService turmaService;
        public TurmasController(ITurmaService turmaService)
        {
            this.turmaService = turmaService;
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

        //GET ALL Turmas
        //GET: /api/turmas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var turmas = await turmaService.GetAllAsync();

            return Ok(turmas);
        }

        // GET BY ID Turmas
        // GET: /api/turmas/{id}

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var resultado = await turmaService.GetByIdAsync(Id);
            if (!resultado.Success) { return ErrorMapping(resultado); }

            return Ok(resultado.Dados);
        }

        // POST Turmas
        // POST: /api/turmas
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TurmaAddRequestDto turmaAddRequestDto)
        {
            var resultado = await turmaService.CreateAsync(turmaAddRequestDto);
            if (!resultado.Success) { return ErrorMapping(resultado); }

            return CreatedAtAction(nameof(GetById), new { id = resultado.Dados!.Id }, resultado.Dados);
        }

        // UPDATE Turmas
        // PUT: /api/turmas/{Id}
        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] TurmaUpdateRequestDto turmaUpdateRequestDto)
        {
            var resultado = await turmaService.UpdateAsync(Id, turmaUpdateRequestDto);
            if (!resultado.Success) { return ErrorMapping(resultado); }

            return Ok(resultado.Dados);
        }

        // DELETE Turmas
        // DELETE:/api/turmas/{Id}
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var resultado = await turmaService.DeleteAsync(Id);
            if (!resultado.Success) { return ErrorMapping(resultado); }

            return NoContent();
        }
    }
}
