using Microsoft.AspNetCore.Mvc;
using CandidateAPI.Entities;
using CandidateAPI.Services.AuthService;

namespace CandidateAPI.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _AuthService;
        public AuthController(IAuthService authService)
        {

            _AuthService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
        {
            // 1. Validar y obtener el usuario (ejemplo básico)
            var candidato = _AuthService.AuthCandidato(request.Email, request.Password);

            if (candidato == null)
                return Unauthorized("Credenciales inválidas");


            // 2. Generar el token JWT
            var token = _AuthService.GenerateToken(candidato);

            // 3. Devolver respuesta con usuario y token
            return Ok(new AuthResponse
            {
                Candidato = candidato,
                Token = token
            });
        }

    }
}