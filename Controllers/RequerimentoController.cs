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

        [Authorize(Policy = "Gestor")]
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

                novoRequerimento.UsuarioId = GetUsuarioIdLogado();

                 if (novoRequerimento.TipoRequerimentoId == null)
                    return BadRequest("O campo tipo requerimento deve ser preenchido.");

                _context.Requerimentos.Add(novoRequerimento);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetRequerimentoId), new { id = novoRequerimento.Id }, novoRequerimento);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar requerimento: {ex.Message}");
            }
        }

        [Authorize(Policy = "Gestor")]
        [HttpGet("situacao/{situacao}")]
        public async Task<IActionResult> GetRequerimentosPorSituacao(SituacaoRequerimentoEnum situacao)
        {
            var requerimentos = await _context.Requerimentos
                .Include(r => r.Usuario)
                .Where(r => r.Situacao == situacao)
                .ToListAsync();

            return Ok(requerimentos);
        }

        [Authorize(Policy = "Gestor")]
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

        [Authorize(Policy = "Gestor")]
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

        private int GetUsuarioIdLogado()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "id");

            if (claim == null)
                throw new Exception("Usuário não autenticado.");

            return int.Parse(claim.Value);
        }

    }
}