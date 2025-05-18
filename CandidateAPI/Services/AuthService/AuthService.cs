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

            var email = candidato?.CorreoElectronico;
            var role = candidato?.Role;

            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, email),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key_your_super_secret_key"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(130),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public Candidato AuthCandidato(string email, string password)
        {
            var user = _context.Candidatos.FirstOrDefault(x => x.CorreoElectronico == email && x.Password == password);
            if (user != null)
            {
            user.Role = "Candidato";
                return user;
            }
            else
            {
                return null;
            }
        }
    }

