using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Data;
using LockAi.Dtos;
using LockAi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;


namespace LockAi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly DataContext _context;

        public UsuarioController(DataContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioId(int id)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                .Include(u => u.RepresentanteLegal)
                .FirstOrDefaultAsync(uBusca => uBusca.Id == id);

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar usuário: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaDto dados)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(dados.UsuarioId);

                if (usuario == null)
                {
                    return NotFound("Usuario não encontrado");
                }

                if (!BCrypt.Net.BCrypt.Verify(dados.SenhaAtual, usuario.Senha))
                {
                    return BadRequest("Senha atual incorreta");
                }

                if (string.IsNullOrWhiteSpace(dados.NovaSenha) || dados.NovaSenha.Length < 6)
                {
                    {
                        return BadRequest("Nova senha deve ter ao menos 6 caracteres");
                    }
                }

                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(dados.NovaSenha);

                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();

                return Ok("Senha altera com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro INTERNO: " + ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddUsuario([FromBody] Usuario novoUsuario)
        {
            try
            {
                ValidarUsuario(novoUsuario); //vai usar o metodo de validacao para validar o usuario e o representante

                novoUsuario.Senha = BCrypt.Net.BCrypt.HashPassword(novoUsuario.Senha);

                _context.Usuarios.Add(novoUsuario); //adiciona no banco
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUsuarioId), new { id = novoUsuario.Id }, novoUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirUsuario(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);

                if (usuario == null)
                {
                    return NotFound("Usuario não encontrado");
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                return Ok("Usuario deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        public void ValidarUsuario(Usuario usuario)
        {
            var atual = DateTime.Today; // Pega a data atual
            var idade = atual.Year - usuario.DtNascimento.Year; // Calcula idade pelo ano

            if (usuario.DtNascimento.Date > atual.AddYears(-idade)) // Ajusta se ainda não fez aniversário este ano
                idade--;

            if (idade < 18 && usuario.RepresentanteLegalId == null) // Se menor de 18 e sem representante
            {
                throw new Exception("Usuários menores de 18 anos devem ter representante legal.");
            }

            if (idade >= 18 && usuario.RepresentanteLegalId != null) // Se maior ou igual a 18 com representante
            {
                throw new Exception("Usuários maiores de 18 anos não devem ter representante legal.");
            }
        }

    }
}