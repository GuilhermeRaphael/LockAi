using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LockAi.Data;
using LockAi.Models;
using LockAi.Models.Enuns;

namespace LockAi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocacaoController : ControllerBase
    {
        private readonly DataContext _context;

        public LocacaoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")] // Busca por ID
        public async Task<IActionResult> GetLocacaoId(int id)
        {
            try
            {
                var locacao = await _context.Locacao.FindAsync(id);

                if (locacao == null)
                    return NotFound();

                return Ok(locacao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarLocacao([FromBody] Locacao novaLocacao)
        {
            try
            {
                _context.Locacao.Add(novaLocacao);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetLocacaoId), new { id = novaLocacao.Id }, novaLocacao);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar locação: {ex.Message}");
            }
        }

        [HttpPut("cancelar/{id}")]
        public async Task<IActionResult> CancelarLocacao(int id)
        {
            try
            {
                var locacao = await _context.Locacao.FindAsync(id);

                if (locacao == null)
                    return NotFound("Locação não encontrada.");

                if (locacao.Situacao == SituacaoLocacaoEnum.Cancelada)
                    return NotFound("Locação já está cancelada.");

                locacao.Situacao = SituacaoLocacaoEnum.Cancelada;
                locacao.DataSituacao = DateTime.Now.ToString("yyyy-MM-dd");
                locacao.IdUsuarioSituacao = /* ID do usuário que cancelou (pode vir do token ou da requisição) */ 1;


                _context.Locacao.Update(locacao);
                await _context.SaveChangesAsync();

                return Ok("Locação cancelada com sucesso.");
            }
            catch (Exception ex)
                {
                    return StatusCode(500, $"Erro ao cancelar locação: {ex.Message}");
                }
        }


    }
}