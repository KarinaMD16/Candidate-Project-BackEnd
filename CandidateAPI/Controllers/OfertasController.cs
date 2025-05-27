using CandidateAPI.DTOs;
using CandidateAPI.JWTDataBase;
using CandidateAPI.Services.Ofertas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class OfertasController : ControllerBase
{
    private readonly IOfertaService _ofertaService;

    public OfertasController(IOfertaService ofertaService)
    {
        _ofertaService = ofertaService;
    }

    [HttpPost("crear")]
    public async Task<IActionResult> CrearOferta([FromBody] CrearOfertaDTO dto)
    {
        try
        {
            var nuevaOferta = await _ofertaService.CrearOfertaAsync(dto);
            return Ok(nuevaOferta);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            // Para errores no controlados
            return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
        }
    }

    [HttpGet]
    public ActionResult<List<OfertaDto>> Get()
    {
        var ofertas = _ofertaService.GetOfertasConHabilidades();
        return Ok(ofertas);
    }

    [HttpGet("candidato/{id}/ofertas")]
    public IActionResult GetOfertasPorCandidato(int id)
    {
        var ofertas = _ofertaService.GetOfertasPorCandidato(id);
        return Ok(ofertas);
    }

}