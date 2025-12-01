using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LockAi.Data;
using LockAi.Dtos;
using LockAi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;



namespace LockAi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;


        public AuthController(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Login == login.Login);

                if (usuario == null || !BCrypt.Net.BCrypt.Verify(login.Senha, usuario.Senha))
                {
                    return Unauthorized("Login ou senha inválidos.");
                }

                var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

                var claims = new[]
                  {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Login),
                    new Claim("tipo", usuario.TipoUsuarioId.ToString())
                  };

                var creds = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                );

                var token = new JwtSecurityToken(
                    expires: DateTime.UtcNow.AddHours(2),
                    claims: claims,
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    message = "Login realizado com sucesso",
                    token = tokenString,
                    usuario = new
                    {
                        usuario.Id,
                        usuario.Login,
                        tipoUsuarioId = usuario.TipoUsuarioId
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}