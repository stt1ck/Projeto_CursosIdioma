using Application.ProjetoCursosIdioma.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.ProjetoCursosIdioma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NiveisTurmaController : ControllerBase
    {
        private readonly INivelTurmaService _nivelTurmaService;

        public NiveisTurmaController(INivelTurmaService nivelTurmaService)
        {
            _nivelTurmaService = nivelTurmaService;
        }

        // GET ALL Niveis
        // GET: /api/niveis
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var niveis = await _nivelTurmaService.GetAllAsync();

            return Ok(niveis);
        }
    }
}
