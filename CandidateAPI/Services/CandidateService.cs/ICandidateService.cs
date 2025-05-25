using CandidateAPI.DTOs;
using CandidateAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace CandidateAPI.Services.CandidateService
{
    public interface ICandidateService
    {
        public Candidato AddCandidato(RegisterRequest candidato);
        public Task<CandidatoDTO> GetUserProfile(int userId);
        public Task<bool> getUserbyemail(string email);

        Task<bool> AgregarHabilidadesUsuario(int candidatoId, int habilidadId);

        bool PostularACandidatura(int candidatoId, int ofertaId);

        List<OfertaDto> ObtenerOfertasPostuladas(int candidatoId);

    }

}
