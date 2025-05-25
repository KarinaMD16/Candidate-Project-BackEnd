using CandidateAPI.DTOs;
using CandidateAPI.JWTDataBase;
using Microsoft.EntityFrameworkCore;

namespace CandidateAPI.Services.Habilidades
{
    public class HabilidadService: IHabilidadesService
    {
        private readonly JWTDbContext _jwtDbContext;

        public HabilidadService(JWTDbContext jwtDbContext)
        {
            _jwtDbContext = jwtDbContext;
        }

        public IEnumerable<HabilidadDto> GetTodasHabilidades()
        {
            return _jwtDbContext.Habilidades
                .Select(c => new HabilidadDto
                {
                    Id = c.Id,
                    name = c.Nombre
                })
                .ToList();
        }
    }
}
