using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LockAi.Data;
using LockAi.Dtos;
using LockAi.Models;
using Microsoft.EntityFrameworkCore;


namespace LockAi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthController(DataContext context)
        {
            _context = context;
        }

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

                return Ok(new { message = "Login realizado com sucesso", usuario.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}