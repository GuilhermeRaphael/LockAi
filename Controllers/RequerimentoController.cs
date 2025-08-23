using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LockAi.Data;
using LockAi.Models;
using LockAi.Models.Enuns;

namespace LockAi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequerimentoController : ControllerBase
    {
        private readonly DataContext _context;

        public RequerimentoController(DataContext context)
        {
            _context = context;
        }

        // Listar todos os requerimentos com o usuário solicitante
        [HttpGet]
        public async Task<IActionResult> GetRequerimentos()
        {
            try
            {
                var lista = await _context.Requerimentos
                    .Include(r => r.Usuario)
                    .ToListAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Consultar requerimento por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequerimentoId(int id)
        {
            try
            {
                var requerimento = await _context.Requerimentos
                    .Include(r => r.Usuario)
                    .FirstOrDefaultAsync(rBusca => rBusca.Id == id);

                if (requerimento == null)
                    return NotFound("Requerimento não encontrado.");

                return Ok(requerimento);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar o requerimento: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarRequerimento([FromBody] Requerimento novoRequerimento)
        {
            try
            {
                if (novoRequerimento == null)
                    return BadRequest("Dados inválidos.");

                _context.Requerimentos.Add(novoRequerimento);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetRequerimentoId), new { id = novoRequerimento.Id }, novoRequerimento);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar requerimento: {ex.Message}");
            }
        }

        [HttpGet("situacao/{situacao}")]
        public async Task<IActionResult> GetRequerimentosPorSituacao(SituacaoRequerimentoEnum situacao)
        {
            var requerimentos = await _context.Requerimentos
                .Include(r => r.Usuario)
                .Where(r => r.Situacao == situacao)
                .ToListAsync();

            return Ok(requerimentos);
        }
    }
}