using CandidateAPI.JWTDataBase;
using Microsoft.AspNetCore.Mvc;

namespace CandidateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly JWTDbContext _context;

        public EmpresaController(JWTDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CrearEmpresa([FromBody] Empresa empresa)
        {
            if (empresa == null || string.IsNullOrEmpty(empresa.Nombre))
            {
                return BadRequest("Datos de empresa inválidos.");
            }

            _context.Empresas.Add(empresa);
            _context.SaveChanges();

            return Ok(empresa);
        }
    }
}