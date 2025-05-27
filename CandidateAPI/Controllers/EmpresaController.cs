using CandidateAPI.DTOs;
using CandidateAPI.JWTDataBase;
using CandidateAPI.Services.Empresas;
using Microsoft.AspNetCore.Mvc;

namespace CandidateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _service;

        public EmpresaController(IEmpresaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CrearEmpresa([FromBody] CrearEmpresaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resulatdo = await _service.crearEmpresa(dto);
            return Ok(resulatdo);
        }
    }
}