using Microsoft.AspNetCore.Mvc;
using CandidateAPI.JWTDataBase;
using CandidateAPI.DTOs;
using CandidateAPI.Services.Habilidades;

namespace CandidateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HabilidadController : ControllerBase
    {
        private readonly IHabilidadesService _habilidadaService;

        public HabilidadController(IHabilidadesService habilidadesService)
        {
            _habilidadaService = habilidadesService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<HabilidadDto>> ObtenerTodos()
        {
            return Ok(_habilidadaService.GetTodasHabilidades());
        }
    }
}