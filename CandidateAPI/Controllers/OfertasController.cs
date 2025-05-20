using CandidateAPI.DTOs;
using CandidateAPI.JWTDataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class OfertasController : ControllerBase
{
    private readonly JWTDbContext _context;

    public OfertasController(JWTDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CrearOferta([FromBody] CrearOfertaDTO dto)
    {
        // se verifica que la empresa exista
        var empresa = await _context.Empresas.FindAsync(dto.EmpresaId);
        if (empresa == null)
        {
            return NotFound("Empresa no encontrada");
        }

        // creamos la oferta
        var oferta = new Oferta
        {
            Puesto = dto.Puesto,
            Descripcion = dto.Descripcion,
            EmpresaId = dto.EmpresaId
        };

        // agregamos la oferta a la base de datos para obtener su id
        _context.Ofertas.Add(oferta);
        await _context.SaveChangesAsync();

        // asociamos las habilidades seleccionadas
        foreach (var habilidadId in dto.HabilidadesIds)
        {
            var habilidadExiste = await _context.Habilidades.AnyAsync(h => h.Id == habilidadId);
            if (!habilidadExiste)
            {
                return BadRequest($"La habilidad con ID {habilidadId} no existe");
            }

            var ofertaHabilidad = new OfertaHabilidad
            {
                OfertaId = oferta.Id,
                HabilidadId = habilidadId
            };

            _context.OfertaHabilidades.Add(ofertaHabilidad);
        }

        await _context.SaveChangesAsync();

        return Ok(new { mensaje = "Oferta creada exitosamente", oferta.Id });
    }
}