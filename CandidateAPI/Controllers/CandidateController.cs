using CandidateAPI.DTOs;
using CandidateAPI.Entities;
using CandidateAPI.Services.CandidateService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace CandidateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        // GET: api/<CandidateController>
        [HttpGet("perfil")]
        [Authorize(Roles = "Candidato")]
        public async Task<IActionResult> GetProfile()
        {
            var userIdfind = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdfind, out var userId)) return Unauthorized();

            var userProfile = await _candidateService.GetUserProfile(userId);
            if (userProfile == null) return NotFound();

            return Ok(userProfile);

        }

        // POST api/<CandidateController>
        [HttpPost("register")]
        public Candidato Post([FromBody] RegisterRequest candidato)
        {
            return _candidateService.AddCandidato(candidato);
        }
        [HttpGet("find")]
        public async Task<bool> GetUserbyemail(string email)
        {
            var userExists = await _candidateService.getUserbyemail(email);
            return userExists;
        }

        [HttpPost("asociarHabilidad")]
        public async Task<IActionResult> AgregarHabilidad(int candidatoId, int habilidadId)
        {
            var result = await _candidateService.AgregarHabilidadesUsuario(candidatoId, habilidadId);

            if (!result)
                return NotFound("Candidato o habilidad no encontrado");

            return Ok("Habilidad agregada correctamente");
        }

        [HttpPost("postular")]
        public IActionResult PostularACandidatura([FromBody] PostulacionDto dto)
        {
            var resultado = _candidateService.PostularACandidatura(dto.CandidatoId, dto.OfertaId);

            if (!resultado)
                return BadRequest("No se pudo realizar la postulación");

            return Ok("Postulación exitosa.");
        }

        [HttpGet("{candidatoId}/postulaciones")]
        public IActionResult GetOfertasPostuladas(int candidatoId)
        {
            var ofertas = _candidateService.ObtenerOfertasPostuladas(candidatoId);

            if (ofertas == null || !ofertas.Any())
                return NotFound("El candidato no tiene postulaciones registradas.");

            return Ok(ofertas);
        }


    }
}