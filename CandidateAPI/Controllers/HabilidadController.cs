using Microsoft.AspNetCore.Mvc;
using CandidateAPI.JWTDataBase;

namespace CandidateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HabilidadController : ControllerBase
    {
        private readonly JWTDbContext _context;

        public HabilidadController(JWTDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetHabilidades()
        {
            var habilidades = _context.Habilidades.ToList();
            return Ok(habilidades);
        }
    }
}