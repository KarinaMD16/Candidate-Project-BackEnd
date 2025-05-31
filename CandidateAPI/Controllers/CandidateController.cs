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
        [Authorize(Roles = "Candidato")]
        public async Task<IActionResult> AgregarHabilidad(int candidatoId, int habilidadId)
        {
            var userIdFromToken = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdFromToken, out var userId) || userId != candidatoId)
            {
                return Unauthorized("No tienes permiso para modificar a este candidato.");
            }

            var result = await _candidateService.AgregarHabilidadesUsuario(candidatoId, habilidadId);

            if (!result)
                return NotFound("Candidato o habilidad no encontrado");

            return Ok("Habilidad agregada correctamente");
        }

        [HttpPost("postular")]
        [Authorize(Roles = "Candidato")]
        public IActionResult PostularACandidatura([FromBody] PostulacionDto dto)
        {

            var userIdFromToken = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdFromToken, out var userId) || userId != dto.CandidatoId)
            {
                return Unauthorized("No tienes permiso para modificar a este candidato.");
            }

            var resultado = _candidateService.PostularACandidatura(dto.CandidatoId, dto.OfertaId);

            if (!resultado)
                return BadRequest("No se pudo realizar la postulación");

            return Ok("Postulación exitosa.");
        }

        [HttpGet("{candidatoId}/postulaciones")]
        [Authorize(Roles = "Candidato")]
        public IActionResult GetOfertasPostuladas(int candidatoId)
        {
            var userIdFromToken = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdFromToken, out var userId) || userId != candidatoId)
            {
                return Unauthorized("No tienes permiso para ver la información de este candidato.");
            }

            var ofertas = _candidateService.ObtenerOfertasPostuladas(candidatoId);

            // Devolvemos una lista vacía del mismo tipo
            return Ok(ofertas ?? new List<OfertaDto>());
        }

        [HttpDelete("{candidatoId}/eliminarHabilidad/{habilidadId}")]
        [Authorize(Roles = "Candidato")]
        public async Task<IActionResult> EliminarHabilidad(int candidatoId, int habilidadId)
        {

            var userIdFromToken = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdFromToken, out var userId) || userId != candidatoId)
            {
                return Unauthorized("No tienes permiso para modificar a este candidato.");
            }

            var result = await _candidateService.EliminarHabilidadDeCandidato(candidatoId, habilidadId);

            if (!result)
                return NotFound("El candidato o la habilidad no fueron encontrados.");

            return NoContent();
        }

        [HttpDelete("{candidatoId}/eliminiarPostulacion/{ofertaId}")]
        [Authorize(Roles = "Candidato")]
        public async Task<IActionResult> EliminarOferta(int candidatoId, int ofertaId)
        {

            var userIdFromToken = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdFromToken, out var userId) || userId != candidatoId)
            {
                return Unauthorized("No tienes permiso para modificar a este candidato.");
            }

            var result = await _candidateService.EliminarOfertaCandidato(candidatoId, ofertaId);

            if (!result)
                return NotFound("El candidato o la oferta no fueron encontrados.");

            return NoContent();
        }
    }
}