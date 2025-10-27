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
    public class LocacaoParceiroController :ControllerBase
    {
         private DataContext _context;

        public LocacaoParceiroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParceiroById (int id)
        {
              try
            {
                var parceiroLoc = await _context.LocacoesParceiro.FirstOrDefaultAsync(r => r.IdLocacao == id);

                if (parceiroLoc == null)
                    return NotFound($"Não á parceiro para esta locacão.");

                return Ok(parceiroLoc);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Erro ao buscar parceiro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddParceiro(LocacaoParceiro locacaoParceiro)
        {
            try
            {

                locacaoParceiro.Situacao = SituacaoLocacaoParceiroEnum.Pendente;
                locacaoParceiro.DtSituacao = DateTime.Now;
                var usuario = await GetUsuarioLogadoAsync();
                locacaoParceiro.IdUsuarioSituacao = usuario.Id;

                _context.LocacoesParceiro.Add(locacaoParceiro);
                await _context.SaveChangesAsync();
                 return CreatedAtAction(nameof(GetParceiroById), new { id = locacaoParceiro.IdLocacao }, locacaoParceiro);


            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar parceiro. {ex.Message}");
            }

        }

        private async Task<Usuario> GetUsuarioLogadoAsync()
        {
            return await _context.Usuarios.FindAsync(1); // ID fixo por enquanto, mudar com a implementação do JWT
        }


          [HttpDelete("{idLocacao}/{idParceiro}")]
        public async Task<IActionResult> ExcluirLocacaoParceiro(int idLocacao, int idParceiro)
        {
            try
            {
                var associado = await _context.LocacoesParceiro.FirstOrDefaultAsync(r => r.IdLocacao == idLocacao && r.IdParceiro == idParceiro);

                if (associado == null)
                    return NotFound($"Parceiro não encontrado para esta locacão.");

                associado.Situacao = SituacaoLocacaoParceiroEnum.Inativo;
                associado.DtSituacao = DateTime.Now;
                var usuario = await GetUsuarioLogadoAsync();
                associado.IdUsuarioSituacao = usuario.Id;

                _context.LocacoesParceiro.Update(associado);
                await _context.SaveChangesAsync();

                return Ok($"Parceiro inativo com sucesso.");

            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhe = ex.Message });
            }
        }

    }
}