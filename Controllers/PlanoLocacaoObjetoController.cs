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
    public class PlanoLocacaoObjetoController : ControllerBase
    {
        private readonly DataContext _context;

        public PlanoLocacaoObjetoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetId/{idPlano}")]
        public async Task<IActionResult> GetIdPlanoById(int idPlano)
        {
            try
            {
                var listPlanoLocacaoObjeto = await _context.PlanosLocacoesObjeto
                .Where(p => p.IdPlanoLocacao == idPlano)
                .ToListAsync();

                if (listPlanoLocacaoObjeto == null)
                    return NotFound("Nenhuma associação encontrada para este plano.");

                return Ok(listPlanoLocacaoObjeto);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPlanoLocacaoObjeto(PlanoLocacaoObjeto novoplanoLocacaoObjeto)
        {
            try
            {
                novoplanoLocacaoObjeto.Situacao = SituacaoPlanoLocacaoObjeto.Vinculado;
                novoplanoLocacaoObjeto.DtInclusao = DateTime.Now;

                var usuario = await GetUsuarioLogadoAsync();
                novoplanoLocacaoObjeto.IdUsuarioInclusao = usuario.Id;

                _context.PlanosLocacoesObjeto.Add(novoplanoLocacaoObjeto);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetIdPlanoById), new { id = novoplanoLocacaoObjeto.Id }, novoplanoLocacaoObjeto);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        private async Task<Usuario> GetUsuarioLogadoAsync()
        {
            return await _context.Usuarios.FindAsync(1); // ID fixo por enquanto, mudar com a implementação do JWT
        }

        [HttpGet("GetIdTipoObjeto/{idTipoObjeto}")]
        public async Task<IActionResult> GetIdTipoObjetoyId(int idTipoObjeto)
        {
            try
            {
                var listTipoObjeto = await _context.PlanosLocacoesObjeto
                .Where(p => p.IdTipoObjeto == idTipoObjeto)
                .ToListAsync();

                if (listTipoObjeto == null)
                    return NotFound("Nenhuma associação encontrada para este tipo de objeto.");

                return Ok(listTipoObjeto);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("plano/{idPlano}/tipo/{idTipoObjeto}")]
        public async Task<IActionResult> DeletePlanoTipo(int idPlano, int idTipoObjeto)
        {
            try
            {
                // Busca a associação específica
                var associacao = await _context.PlanosLocacoesObjeto
                    .FirstOrDefaultAsync(p => p.IdPlanoLocacao == idPlano && p.IdTipoObjeto == idTipoObjeto);

                if (associacao == null)
                    return NotFound(new { mensagem = "Associação não encontrada." });

                associacao.Situacao = SituacaoPlanoLocacaoObjeto.Desassociado;
                associacao.DtAtualizacao = DateTime.Now;

                var usuario = await GetUsuarioLogadoAsync();
                associacao.IdUsuarioAtualizacao = usuario.Id;

                _context.PlanosLocacoesObjeto.Update(associacao);
                await _context.SaveChangesAsync();
             
                return Ok(new { mensagem = "Associação desativada com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhe = ex.Message });
            }
        }

        

        
    }
}