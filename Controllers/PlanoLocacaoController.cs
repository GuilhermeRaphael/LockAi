using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Data;
using LockAi.Models;
using LockAi.Models.Enuns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace LockAi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class PlanoLocacaoController : ControllerBase
    {
        private readonly DataContext _context;

        public PlanoLocacaoController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlanoLocacaoById(int id)
        {
            try
            {
                var plano = await _context.PlanosLocacao
                    .Include(t => t.UsuarioInclusao)
                    .Include(t => t.UsuarioAtualizacao)
                    .Include(t => t.PlanoLocacaoObjetos)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (plano == null)
                    return NotFound("Plano de locação não encontrado.");

                
                if (DateTime.Now > plano.DtFim && plano.Situacao != SituacaoPlanoLocacao.Inativo)
                {
                    plano.Situacao = SituacaoPlanoLocacao.Inativo;
                    plano.DtAtualizacao = DateTime.Now;

                    _context.PlanosLocacao.Update(plano);
                    await _context.SaveChangesAsync();
                }

                return Ok(plano);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar o plano locação: {ex.Message}");
            }
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetPlanoLocacao()
        {
            try
            {
                var lista = await _context.PlanosLocacao
                        .Include(t => t.UsuarioInclusao)
                        .Include(t => t.UsuarioAtualizacao)
                        .Include(t => t.PlanoLocacaoObjetos)
                        .ToListAsync();

                foreach (var plano in lista)
                {
                    if (DateTime.Now > plano.DtFim && plano.Situacao != SituacaoPlanoLocacao.Inativo)
                    {
                        plano.Situacao = SituacaoPlanoLocacao.Inativo;
                        plano.DtAtualizacao = DateTime.Now;
                        // ajuste opcional:
                        // plano.IdUsuarioAtualizacao = ??? // aqui só se você souber o usuário

                        _context.PlanosLocacao.Update(plano);
                    }
                }

                await _context.SaveChangesAsync();
                return Ok(lista);

                
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Erro ao buscar plano locação: {ex.Message}");
            }
        }

        [Authorize(Policy = "Gestor")]
        [HttpPost]
        public async Task<IActionResult> AddPlanoLocacao(PlanoLocacao novoPlanoLocacao)
        {
            try
            {
                var usuario = await GetUsuarioLogadoAsync();
                if (usuario == null)
                return Unauthorized("Usuário não identificado.");

                novoPlanoLocacao.DtInclusao = DateTime.Now;
                novoPlanoLocacao.DtAtualizacao = DateTime.Now;

                novoPlanoLocacao.IdUsuarioInclusao = usuario.Id;
                novoPlanoLocacao.IdUsuarioAtualizacao = usuario.Id;

                _context.PlanosLocacao.Add(novoPlanoLocacao);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPlanoLocacaoById), new { id = novoPlanoLocacao.Id }, novoPlanoLocacao);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar novo plano de locacao: {ex.Message}");
            }
        }

        [Authorize(Policy = "Gestor")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirPlanoLocacao(int id)
        {
            try
            {
                var usuario = await GetUsuarioLogadoAsync();
                if (usuario == null)
                    return Unauthorized("Usuário logado não encontrado.");

                PlanoLocacao planoLocacao = await _context.PlanosLocacao.FindAsync(id);

                if (planoLocacao == null)
                    return NotFound("Plano locação não encontrado.");

                planoLocacao.Situacao = SituacaoPlanoLocacao.Inativo;
                planoLocacao.DtAtualizacao = DateTime.Now;
                planoLocacao.IdUsuarioAtualizacao = usuario.Id;

                _context.PlanosLocacao.Update(planoLocacao);
                await _context.SaveChangesAsync();

                return Ok(new {
                    message = "Plano de locação inativo com sucesso.",
                    plano = planoLocacao
                }); 
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao alterar situação do plano de locacao. {ex.Message}");
            }
        }

        private async Task<Usuario> GetUsuarioLogadoAsync()
        {
            var userIdClaim =  User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return null;

            int userId = int.Parse(userIdClaim.Value);

            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}