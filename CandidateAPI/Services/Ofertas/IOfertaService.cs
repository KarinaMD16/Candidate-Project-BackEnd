using CandidateAPI.DTOs;

namespace CandidateAPI.Services.Ofertas
{
    public interface IOfertaService
    {
        Task<Oferta> CrearOfertaAsync(CrearOfertaDTO dTO);

        List<OfertaDto> GetOfertasConHabilidades();

        List<OfertaDto> GetOfertasPorCandidato(int candidatoId);

    }
}
