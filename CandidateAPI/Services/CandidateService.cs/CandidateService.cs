using CandidateAPI.JWTDataBase;
using CandidateAPI.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CandidateAPI.DTOs;


namespace CandidateAPI.Services.CandidateService
{
    public class CandidateService : ICandidateService
    {
        private readonly JWTDbContext _context;
        public CandidateService(JWTDbContext context)
        {
            _context = context;
        }

        public Candidato AddCandidato(RegisterRequest candidato)
        {
            // Validar el candidato 
            if (string.IsNullOrEmpty(candidato.Nombre) || string.IsNullOrEmpty(candidato.Apellido) || string.IsNullOrEmpty(candidato.CorreoElectronico) || string.IsNullOrEmpty(candidato.Password))
            {
                throw new ArgumentException("Los campos no pueden estar vacíos");
            }


            else
            {
                Candidato newCandidato = new Candidato();
                newCandidato.Nombre = candidato.Nombre;
                newCandidato.Apellido = candidato.Apellido;
                newCandidato.CorreoElectronico = candidato.CorreoElectronico;
                newCandidato.Password = candidato.Password;
                newCandidato.Role = "Candidato";

                _context.Candidatos.Add(newCandidato);
                _context.SaveChanges();
                return newCandidato;
            }
        }

        public async Task<CandidatoDTO?> GetUserProfile(int userId)
        {
            var user = await _context.Candidatos.FindAsync(userId);
            if (user == null) return null;

            return new CandidatoDTO
            {
                id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                CorreoElectronico = user.CorreoElectronico,
            };
        }
        public async Task<bool> getUserbyemail(string email)
        {
            var user = await _context.Candidatos.FirstOrDefaultAsync(c => c.CorreoElectronico == email);
            if (user == null) return false;

            return true;
        }

        public async Task<bool> AgregarHabilidadesUsuario(int candidatoId, int habilidadId)
        {
            var candidato = await _context.Candidatos
                .Include(c => c.Habilidades)
                .FirstOrDefaultAsync(c => c.Id == candidatoId);

            if (candidato == null)
                return false; // Candidato no encontrado

            var habilidad = await _context.Habilidades.FindAsync(habilidadId);

            if (habilidad == null)
                return false; // Habilidad no encontrada

            // Evitar agregar duplicados
            if (!candidato.Habilidades.Any(h => h.Id == habilidadId))
            {
                candidato.Habilidades.Add(habilidad);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public bool PostularACandidatura(int candidatoId, int ofertaId)
        {
            var candidato = _context.Candidatos
                .Include(c => c.OfertasAplicadas)
                .FirstOrDefault(c => c.Id == candidatoId);

            var oferta = _context.Ofertas.FirstOrDefault(o => o.Id == ofertaId);

            if (candidato == null || oferta == null)
                return false;

            if (candidato.OfertasAplicadas.Any(o => o.Id == ofertaId))
                return false; // Ya está postulado

            candidato.OfertasAplicadas.Add(oferta);
            _context.SaveChanges();

            return true;
        }

        public List<OfertaDto> ObtenerOfertasPostuladas(int candidatoId)
        {
            var candidato = _context.Candidatos
                .Include(c => c.OfertasAplicadas)
                    .ThenInclude(o => o.Empresa)
                .Include(c => c.OfertasAplicadas)
                    .ThenInclude(o => o.OfertaHabilidades)
                        .ThenInclude(oh => oh.Habilidad)
                .FirstOrDefault(c => c.Id == candidatoId);

            if (candidato == null)
                return new List<OfertaDto>();

            var ofertas = candidato.OfertasAplicadas.Select(o => new OfertaDto
            {
                Id = o.Id,
                Puesto = o.Puesto,
                Descripcion = o.Descripcion,
                EmpresaNombre = o.Empresa.Nombre,
                Habilidades = o.OfertaHabilidades.Select(oh => new HabilidadDto
                {
                    Id = oh.Habilidad.Id,
                    name = oh.Habilidad.Nombre
                }).ToList()
            }).ToList();

            return ofertas;
        }


    }
}
