using CandidateAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace CandidateAPI.Services.CandidateService
{
    public interface ICandidateService
    {
        public Candidato AddCandidato(Candidato candidato);
        public List<Candidato> GetCandidatos();


    }
}
