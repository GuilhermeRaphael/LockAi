using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Data;
using LockAi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LockAi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PlanoLocacaoController : ControllerBase
    {
        private readonly DataContext _context;

        public PlanoLocacaoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlanoLocacaoById(int id)
        {
            try
            {
                PlanoLocacao planolocacao = await _context.PlanosLocacao.FirstOrDefaultAsync(r => r.Id == id);

                if (planolocacao == null)
                    return NotFound("PlanoLocacão não encontrado.");

                return Ok(planolocacao);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Erro ao buscar o plano locacão{ex.Message}");
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetPlanoLocacao()
        {
            try
            {
                var lista = await _context.PlanosLocacao
                        .Include(t => t.UsuarioInclusao)
                        .Include(t => t.IdUsuarioAtualizacao)
                        .Include(t => t.PlanoLocacaoObjetos)
                        .ToListAsync();

                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Erro ao buscar plano locação: {ex.Message}");
            }
        }
    }
}