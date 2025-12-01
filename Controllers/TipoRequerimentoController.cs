using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Data;
using LockAi.Dtos;
using LockAi.Models;
using LockAi.Models.Enuns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;


namespace LockAi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TipoRequerimentoController : ControllerBase
    {
        private readonly DataContext _context;

        public TipoRequerimentoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoRequerimentoById(int id)
        {
            try
            {
                TipoRequerimento tipoRequerimento = await _context.TiposRequerimento.FirstOrDefaultAsync(r => r.Id == id);

                if (tipoRequerimento == null)
                    return NotFound("Tipo requerimento não encontrado.");

                return Ok(tipoRequerimento);
            }
            catch (System.Exception ex)
            {

                return BadRequest($"Erro ao buscar Tipo Requerimento {ex.Message}");
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetTipoRquerimento()
        {
            try
            {
                var lista = await _context.TiposRequerimento
                          .Include(t => t.UsuarioInclusao)
                          .Include(t => t.UsuarioAtualizacao)
                          .Include(t => t.Requerimentos)
                          .ToListAsync();

                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Erro ao buscar tipos de requerimento: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTipoRequerimento(TipoRequerimento novoTipoRequerimento)
        {
            try
            {
                novoTipoRequerimento.Situacao = SituacaoTipoRequerimentoEnum.EmAnalise;
                novoTipoRequerimento.DataInclusao = DateTime.Now;
                novoTipoRequerimento.DataAlteracao = DateTime.Now;

                var usuario = await GetUsuarioLogadoAsync();
                novoTipoRequerimento.IdUsuarioInclusao = usuario.Id;
                novoTipoRequerimento.IdUsuarioAtualizacao = usuario.Id;


                _context.TiposRequerimento.Add(novoTipoRequerimento);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetTipoRequerimentoById), new { id = novoTipoRequerimento.Id }, novoTipoRequerimento);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar tipo de requerimento: {ex.Message}");
            }
        }

        private async Task<Usuario> GetUsuarioLogadoAsync()
        {
            return await _context.Usuarios.FindAsync(1); // ID fixo por enquanto, mudar com a implementação do JWT
        }


        [HttpPatch("AlterarValor/{idTipo}")]
        public async Task<IActionResult> PatchAlterarValor(int idTipo, [FromBody] AlterarValorDtos dto)
        {
            var tipo = await _context.TiposRequerimento.FindAsync(idTipo);

            if (tipo == null)
                return BadRequest($"TipoRequerimento com ID {idTipo} não encontrado.");

            tipo.Valor = dto.Valor;
            tipo.DataAlteracao = DateTime.Now;
            tipo.IdUsuarioAtualizacao = dto.IdUsuario;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(tipo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar valor: {ex.Message}");
            }
        }

        // ENDPOINT excluirLogico.
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirTipoRequerimento(int id)
        {
            try
            {
                TipoRequerimento tipoRequerimento = await _context.TiposRequerimento.FindAsync(id);

                if (tipoRequerimento == null)
                    return NotFound("Tipo de requerimento não encontrado.");

                //Exclusão Logica
                tipoRequerimento.Situacao = SituacaoTipoRequerimentoEnum.Excluido;
                tipoRequerimento.DataAlteracao = DateTime.Now;

                var usuario = await GetUsuarioLogadoAsync();
                if (usuario == null)
                    return StatusCode(500, "Usuário logado não encontrado.");

                tipoRequerimento.IdUsuarioAtualizacao = usuario.Id;


                _context.TiposRequerimento.Update(tipoRequerimento);
                await _context.SaveChangesAsync();

                return Ok("Tipo de requerimento excluido com sucesso.");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir tipo de requerimento: {ex.Message}");
            }
        }
    }
}