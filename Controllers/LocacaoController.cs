using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Data;
using LockAi.Models;
using LockAi.Models.Enuns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LockAi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocacaoController : ControllerBase
    {
        private DataContext _context;

        public LocacaoController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddLocacao(Locacao locacao)
        {
            try
            {
                locacao.DataInicio = DateTime.Now;
                var usuario = await GetUsuarioLogadoAsync();
                locacao.IdUsuarioSituacao = usuario.Id;
                locacao.Situacao = SituacaoLocacaoEnum.Ativa;
                locacao.DataSituacao = DateTime.Now;

                _context.Locacoes.Add(locacao);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetLocacaoById), new { id = locacao.Id }, locacao);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar nova Locacao: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocacaoById(int id)
        {
            try
            {
                Locacao locacaoId = await _context.Locacoes.FirstOrDefaultAsync(r => r.Id == id);

                if (locacaoId == null)
                    return NotFound($"Locacão {id} não encontrada.");

                return Ok(locacaoId);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Erro ao buscar locacão. {ex.Message}");
            }
        }

        [HttpPut("{id}/cancelar")]
        
        public async Task<IActionResult> Cancelar (int id)
        {
            try
            {
                var locacaoId = await _context.Locacoes.FindAsync(id);

                if (locacaoId == null)
                    return NotFound($"Locacao {id} não encontrada.");

                locacaoId.Situacao = SituacaoLocacaoEnum.Cancelada;
                await _context.SaveChangesAsync();

                return Ok(locacaoId);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao cancelar locacão {id}. {ex.Message}");
            }
        }
        
        private async Task<Usuario> GetUsuarioLogadoAsync()
        {
            return await _context.Usuarios.FindAsync(1); // ID fixo por enquanto, mudar com a implementação do JWT
        }


    }
}