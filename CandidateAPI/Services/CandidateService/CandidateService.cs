using CandidateAPI.JWTDataBase;
using CandidateAPI.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace CandidateAPI.Services.CandidateService
{
    public class CandidateService : ICandidateService
    {
        private readonly JWTDbContext _context;
        public CandidateService(JWTDbContext context)
        {
            _context = context;
        }

        public Candidato AddCandidato(Candidato candidato)
        {
            _context.Candidatos.Add(candidato);
            _context.SaveChanges();
            return candidato;
        }
        public List<Candidato> GetCandidatos()
        {
            return _context.Candidatos.ToList();
        }


    }
}
