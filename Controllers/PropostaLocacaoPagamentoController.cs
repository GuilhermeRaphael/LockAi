using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LockAi.Data;
using LockAi.Models;
using LockAi.Models.Enuns;
using Microsoft.EntityFrameworkCore;

namespace LockAi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropostaLocacaoPagamentoController : ControllerBase
    {
        private readonly DataContext _context;

        public PropostaLocacaoPagamentoController(DataContext context)
        {
            _context = context;
        }

        [HttpPatch("{id}/confirmar-pagamento")]
        public async Task<IActionResult> AvaliarPagamento(int id,[FromBody] SituacaoPropostaLocacaoPagamento novaSituacao,[FromQuery] int idGestor)
        {
            try
            {
                var pagamento = await _context.PropostaLocacaoPagamento
                    .Include(p => p.PropostaLocacao)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (pagamento == null)
                    return NotFound("Comprovante de pagamento não encontrado.");

                if (novaSituacao != SituacaoPropostaLocacaoPagamento.Aprovado &&
                    novaSituacao != SituacaoPropostaLocacaoPagamento.Reprovado)
                    return BadRequest("Situação inválida. Use 'Aprovado' ou 'Reprovado'.");

                pagamento.Situacao = novaSituacao;
                pagamento.DtConferencia = DateTime.UtcNow;
                pagamento.IdUsuarioConferencia = idGestor;

                var proposta = pagamento.PropostaLocacao;

                if (proposta != null)
                {
                    proposta.DtSituacao = DateTime.UtcNow;

                    if (novaSituacao == SituacaoPropostaLocacaoPagamento.Aprovado)
                    {
                        proposta.Situacao = SituacaoPropostaEnum.Aprovada;
                    }
                    else
                    {
                        proposta.Situacao = SituacaoPropostaEnum.EmAnalise;
                    }
                }

                await _context.SaveChangesAsync();

                return Ok($"Pagamento {novaSituacao.ToString().ToLower()} com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao avaliar pagamento: {ex.Message}");
            }
        }


    }
}