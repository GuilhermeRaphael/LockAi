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
    public class TipoObjetoController : ControllerBase
    {
        private readonly DataContext _context;

        public TipoObjetoController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddTipoObjeto(TipoObjeto novoTipoObjeto)
        {
            try
            {
                novoTipoObjeto.Situacao = SituacaoTipoObjetoEnum.Pendente;
                novoTipoObjeto.DtInclusao = DateTime.Now;

                var usuario = await GetUsuarioLogadoAsync();
                novoTipoObjeto.IdUsuarioInclusao = usuario.Id;

                _context.TiposObjeto.Add(novoTipoObjeto);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTipoObjetoById), new { id = novoTipoObjeto.Id }, novoTipoObjeto);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao adocionar tipo de objeto: {ex.Message}");
            }
        }

        private async Task<Usuario> GetUsuarioLogadoAsync()
        {
            return await _context.Usuarios.FindAsync(1); // ID fixo por enquanto, mudar com a implementação do JWT
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoObjetoById(int id)
        {
            try
            {
                TipoObjeto tipoObjeto = await _context.TiposObjeto.FirstOrDefaultAsync(r => r.Id == id);

                if (tipoObjeto == null)
                    return NotFound("Tipo objeto não encontrado.");

                return Ok(tipoObjeto);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Erro ao buscar o tipo de objeto: {ex.Message}");
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetTipoObjeto()
        {
            try
            {
                var lista = await _context.TiposObjeto
                        .Include(t => t.UsuarioInclusao)
                        .Include(t => t.UsuarioAtualizacao)
                        .ToListAsync();

                return Ok(lista);

            }
            catch (System.Exception ex)
            {
                return BadRequest($"Erro ao buscar tipo objeto: {ex.Message}");

            }
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> ExcluirTipoObjeto(int id)
        {
            try
            {
                TipoObjeto tipoObjeto = await _context.TiposObjeto.FindAsync(id);

                if (tipoObjeto == null)
                    return NotFound("Tipo objeto não encontrado.");
                tipoObjeto.Situacao = SituacaoTipoObjetoEnum.Inativo;
                tipoObjeto.DtAtualizacao = DateTime.Now;

                var usuario = await GetUsuarioLogadoAsync();
                if (usuario == null)
                    return StatusCode(500, "Usuário logado não encontrado.");
                tipoObjeto.IdUsuarioAtualizacao = usuario.Id;

                _context.TiposObjeto.Update(tipoObjeto);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Tipo Objeto inativo com sucesso.",
                    tipoOb = tipoObjeto
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao alterar situação do tipo de objeto. {ex.Message}");
            }
        }
    }
}