using CandidateAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace CandidateAPI.Services.CandidateService
{
    public interface ICandidateService
    {
        public Candidato AddCandidato(RegisterCandidato candidato);
        public Task<DTOCandidato> GetUserProfile(int userId);
        public Task<bool> getUserbyemail(string email);
    }

}
