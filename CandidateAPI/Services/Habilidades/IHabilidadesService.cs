using CandidateAPI.DTOs;

namespace CandidateAPI.Services.Habilidades
{
    public interface IHabilidadesService
    {
        IEnumerable<HabilidadDto> GetTodasHabilidades();
    }
}
