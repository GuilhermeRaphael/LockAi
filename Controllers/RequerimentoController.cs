using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LockAi.Data;
using LockAi.Models;
using LockAi.Models.Enuns;
using Microsoft.AspNetCore.Authorization;

namespace LockAi.Controllers
{
    [Authorize]
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
        [HttpGet("GetAll")]
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

                novoRequerimento.Momento = DateTime.Now;
                novoRequerimento.Situacao = SituacaoRequerimentoEnum.EmAnalise;
                novoRequerimento.DataAtualizacao = DateTime.Now;

                if (novoRequerimento.UsuarioId == null || novoRequerimento.TipoRequerimentoId == null)
                    return BadRequest($"Os campos de Id usuario e tipo requerimento devem ser preenchidos.");

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

        [HttpPut("{id}/aprovar")]
        public async Task<IActionResult> Aprovar(int id)
        {
            var requerimento = await _context.Requerimentos.FindAsync(id);

            if (requerimento == null)
                return NotFound("Requerimento não encontrado.");

            requerimento.Situacao = SituacaoRequerimentoEnum.Aprovado;
            await _context.SaveChangesAsync();

            return Ok(requerimento);
        }

        [HttpPut("{id}/reprovar")]
        public async Task<IActionResult> Reprovar(int id)
        {
            var requerimento = await _context.Requerimentos.FindAsync(id);

            if (requerimento == null)
                return NotFound("Requerimento não encontrado.");

            requerimento.Situacao = SituacaoRequerimentoEnum.Reprovado;
            await _context.SaveChangesAsync();

            return Ok(requerimento);
        }

        
    }
}