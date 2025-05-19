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
        [HttpPost]
        public Candidato Post([FromBody] RegisterCandidato candidato)
        {
            return _candidateService.AddCandidato(candidato);
        }
    }
}
