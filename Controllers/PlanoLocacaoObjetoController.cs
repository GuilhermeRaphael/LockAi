using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Data;
using LockAi.Models;
using LockAi.Models.Enuns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LockAi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PlanoLocacaoObjetoController : ControllerBase
    {
        private readonly DataContext _context;

        public PlanoLocacaoObjetoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetId/{idPlanoLocacao}/{idTipoObjeto}")]
        public async Task<ActionResult<PlanoLocacaoObjeto>> GetPlanoLocacaoObjetoById(int idPlanoLocacao, int idTipoObjeto)
        {
            try
            {
                var listPlanoLocacaoObjeto = await _context.PlanosLocacoesObjeto
                .FirstOrDefaultAsync(p => p.IdPlanoLocacao == idPlanoLocacao && p.IdTipoObjeto == idTipoObjeto);


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
        public async Task<IActionResult> AddPlanoLocacaoObjeto(PlanoLocacaoObjeto novoObjeto)
        {
            try
            {
                var plano = await _context.PlanosLocacao.FindAsync(novoObjeto.IdPlanoLocacao);
                if (plano == null)
                    return BadRequest($"Plano com Id {novoObjeto.IdPlanoLocacao} não encontrado.");

                var tipoObjeto = await _context.TiposObjeto.FindAsync(novoObjeto.IdTipoObjeto);
                if (tipoObjeto == null)
                    return BadRequest($"Tipo de objeto com Id {novoObjeto.IdTipoObjeto} não encontrado.");

                
                var usuario = await GetUsuarioLogadoAsync();

                // Associação já existe
                var existe = await _context.PlanosLocacoesObjeto
                    .AnyAsync(p => p.IdPlanoLocacao == novoObjeto.IdPlanoLocacao &&
                                p.IdTipoObjeto == novoObjeto.IdTipoObjeto);
                //Cond. aplicada aqui:
                if (existe)
                    return BadRequest("Essa associação já existe.");

                    novoObjeto.Situacao = SituacaoPlanoLocacaoObjeto.Vinculado;
                    novoObjeto.DtInclusao = DateTime.Now;
                    novoObjeto.IdUsuarioInclusao = usuario.Id;

                // salva no banco
                _context.PlanosLocacoesObjeto.Add(novoObjeto);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPlanoLocacaoObjetoById), new
                {
                    idPlanoLocacao = novoObjeto.IdPlanoLocacao,       
                    idTipoObjeto = novoObjeto.IdTipoObjeto
                }, novoObjeto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao associar plano de locação a objeto: {ex.Message}");
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
                .Include(p => p.PlanoLocacao)   // carrega o plano associado
                .Include(p => p.TipoObjeto)
                .ToListAsync();

                if (!listTipoObjeto.Any()      )
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