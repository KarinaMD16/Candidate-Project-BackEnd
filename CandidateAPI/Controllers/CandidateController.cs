using CandidateAPI.Entities;
using CandidateAPI.Services.CandidateService;
using Microsoft.AspNetCore.Mvc;


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

        [HttpGet]
        public IEnumerable<Candidato> Get()
        {
            return _candidateService.GetCandidatos();
        }

        // POST api/<CandidateController>
        [HttpPost]
        public Candidato Post([FromBody] Candidato candidato)
        {
           return _candidateService.AddCandidato(candidato);
      
        }

    }
}
