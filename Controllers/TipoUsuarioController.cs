using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Data;
using LockAi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LockAi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly DataContext _context;

        public TipoUsuarioController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoUsuarioById(int id)
        {
            try
            {
                TipoUsuario tipoUsuario = await _context.TiposUsuario
                .Include(t => t.Usuarios)
                .FirstOrDefaultAsync(tBusca => tBusca.Id == id);
                // Retornara o tipo de Usuario,
                // com as informações de usuarios 
                // que sejam do mesmo tipo.

                if (tipoUsuario == null)
                    return NotFound("Tipo de Usuário mão encontrado.");

                return Ok(tipoUsuario);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Erro ao buscar tipo de usuário: {ex.Message}");
            }
        }

        [HttpGet("{GetAll}")] // Lista todos os representates legais
        public async Task<IActionResult> GetTiposUsuario()
        {
            try
            {
                List<TipoUsuario> lista = await _context.TiposUsuario.Include(t => t.Usuarios).ToListAsync();

                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}