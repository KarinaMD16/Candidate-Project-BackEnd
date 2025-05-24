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
                newCandidato.Habilidades = Array.Empty<string>();
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
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                CorreoElectronico = user.CorreoElectronico,
                Habilidades = user.Habilidades 
            };
        }
        public async Task<bool> getUserbyemail(string email)
        {
            var user = await _context.Candidatos.FirstOrDefaultAsync(c => c.CorreoElectronico == email);
            if (user == null) return false;

            return true;
        }

    }
}
