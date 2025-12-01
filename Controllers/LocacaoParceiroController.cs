using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LockAi.Data;
using LockAi.Models;
using LockAi.Models.Enuns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LockAi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocacaoParceiroController : ControllerBase
    {
        private DataContext _context;

        public LocacaoParceiroController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParceiroById(int id)
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddParceiro(LocacaoParceiro locacaoParceiro)
        {
            try
            {
                var usuario = await GetUsuarioLogadoAsync();
                var locacao = await _context.Locacoes.FindAsync(locacaoParceiro.IdLocacao);

                if (locacao == null)
                    return NotFound("Locação não encontrada.");

                if (usuario.TipoUsuarioId == 1 && locacao.IdUsuario != usuario.Id)
                    return Forbid("Você não pode adicionar parceiro em locação de outro usuário.");

                locacaoParceiro.Situacao = SituacaoLocacaoParceiroEnum.Pendente;
                locacaoParceiro.DtSituacao = DateTime.Now;
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

        [Authorize]
        [HttpDelete("{idLocacao}/{idParceiro}")]
        public async Task<IActionResult> ExcluirLocacaoParceiro(int idLocacao, int idParceiro)
        {
            try
            {
                var associado = await _context.LocacoesParceiro
             .FirstOrDefaultAsync(r => r.IdLocacao == idLocacao && r.IdParceiro == idParceiro);

                if (associado == null)
                    return NotFound($"Parceiro não encontrado para esta locação.");

                var usuario = await GetUsuarioLogadoAsync();
                var locacao = await _context.Locacoes.FindAsync(idLocacao);

                if (locacao == null)
                    return NotFound("Locação não encontrada.");

                if (usuario.TipoUsuarioId == 1 && locacao.IdUsuario != usuario.Id)
                    return Forbid("Você não pode remover parceiro de uma locação que não é sua.");

                associado.Situacao = SituacaoLocacaoParceiroEnum.Inativo;
                associado.DtSituacao = DateTime.Now;
                associado.IdUsuarioSituacao = usuario.Id;

                _context.LocacoesParceiro.Update(associado);
                await _context.SaveChangesAsync();

                return Ok("Parceiro inativo com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhe = ex.Message });
            }
        }

        private async Task<Usuario> GetUsuarioLogadoAsync()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                throw new Exception("Usuário não identificado no token");

            int userId = int.Parse(userIdClaim.Value);

            var usuario = await _context.Usuarios.FindAsync(userId);
            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            return usuario;
        }


    }
}