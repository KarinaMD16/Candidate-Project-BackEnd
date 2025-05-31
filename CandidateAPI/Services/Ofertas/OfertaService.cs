using CandidateAPI.DTOs;
using CandidateAPI.Entities;
using CandidateAPI.JWTDataBase;
using Microsoft.EntityFrameworkCore;

namespace CandidateAPI.Services.Ofertas
{
    public class OfertaService: IOfertaService
    {
        private readonly JWTDbContext _context;

        public OfertaService(JWTDbContext context)
        {
            _context = context;
        }

        public async Task<Oferta> CrearOfertaAsync(CrearOfertaDTO dto)
        {
            // Verificar si la empresa existe
            var empresa = await _context.Empresas.FindAsync(dto.EmpresaId);
            if (empresa == null)
                throw new Exception("La empresa no existe");

            // Crear nueva oferta
            var nuevaOferta = new Oferta
            {
                Puesto = dto.Puesto,
                Descripcion = dto.Descripcion,
                EmpresaId = dto.EmpresaId,
                OfertaHabilidades = new List<OfertaHabilidad>()
            };

            // Asociar habilidades existentes
            foreach (var habilidadId in dto.HabilidadesIds)
            {
                var habilidad = await _context.Habilidades.FindAsync(habilidadId);
                if (habilidad == null)
                {
                    throw new Exception("Una de las habilidades no existe");
                }
                if (habilidad != null)
                {
                    nuevaOferta.OfertaHabilidades.Add(new OfertaHabilidad
                    {
                        HabilidadId = habilidadId,
                        Oferta = nuevaOferta
                    });
                }
            }

            _context.Ofertas.Add(nuevaOferta);
            await _context.SaveChangesAsync();

            return nuevaOferta;
        }

        public List<OfertaDto> GetOfertasConHabilidades()
        {
            

            var ofertas = _context.Ofertas
                .Include(o => o.Empresa)
                .Include(o => o.OfertaHabilidades)
                .Include(o => o.OfertaHabilidades)
                    .ThenInclude(oh => oh.Habilidad)
                .Select(o => new OfertaDto
                {
                    Id = o.Id,
                    Puesto = o.Puesto,
                    Descripcion = o.Descripcion,
                    EmpresaId = o.EmpresaId,
                    EmpresaNombre = o.Empresa.Nombre,
                    icono = o.Empresa.icono,
                    Habilidades = o.OfertaHabilidades.Select(oh => new HabilidadDto
                    {
                        Id = oh.Habilidad.Id,
                        name = oh.Habilidad.Nombre,
                        icono = oh.Habilidad.icono
                    }).ToList()
                })
                .ToList();

            return ofertas;
        }
        public List<OfertaDto> GetOfertasPorCandidato(int candidatoId)
        {

            // 1. Obtener IDs de habilidades del candidato
            var habilidadesCandidato = _context.Candidatos
                .Include(c => c.Habilidades)
                .Include(c => c.Habilidades)
                .Where(c => c.Id == candidatoId)
                .SelectMany(c => c.Habilidades.Select(h => h.Id))
                .ToList();

            // 2. Buscar ofertas que tengan al menos una habilidad del candidato
            var ofertas = _context.Ofertas
                .Include(o => o.Empresa)
                .Include(o => o.OfertaHabilidades)
                    .ThenInclude(oh => oh.Habilidad)
                .Where(o => o.OfertaHabilidades.Any(oh => habilidadesCandidato.Contains(oh.Habilidad.Id)))
                .Select(o => new OfertaDto
                {
                    Id = o.Id,
                    Puesto = o.Puesto,
                    Descripcion = o.Descripcion,
                    EmpresaNombre = o.Empresa.Nombre,
                    icono = o.Empresa.icono,
                    Habilidades = o.OfertaHabilidades.Select(oh => new HabilidadDto
                    {
                        Id = oh.Habilidad.Id,
                        name = oh.Habilidad.Nombre,
                        icono = oh.Habilidad.icono
                    }).ToList()
                })
                .ToList();

            return ofertas;
        }

    }
}
