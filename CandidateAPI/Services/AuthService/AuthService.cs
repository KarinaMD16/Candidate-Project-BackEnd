using CandidateAPI.Entities;
using CandidateAPI.JWTDataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CandidateAPI.Services.AuthService;

    public interface IAuthService
    {
        string GenerateToken(Candidato candidato);
        public Candidato AuthCandidato(string email, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly JWTDbContext _context;
        public AuthService(JWTDbContext context)
        {
            _context = context;
        }

        public string GenerateToken(Candidato candidato)
        {
            if (candidato == null || candidato.Id == 0 || string.IsNullOrWhiteSpace(candidato.CorreoElectronico) || string.IsNullOrWhiteSpace(candidato.Role))
                throw new ArgumentException("Candidato inválido al generar token");

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, candidato.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, candidato.CorreoElectronico),
            new Claim(ClaimTypes.Role, candidato.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key_your_super_secret_key"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    public Candidato AuthCandidato(string email, string password)
        {
            var user = _context.Candidatos.FirstOrDefault(x => x.CorreoElectronico == email && x.Password == password);
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }

