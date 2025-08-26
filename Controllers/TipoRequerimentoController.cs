using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Data;
using LockAi.Dtos;
using LockAi.Models;
using LockAi.Models.Enuns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LockAi.Controllers
{
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
                List<TipoRequerimento> lista = await _context.TiposRequerimento
                .Include(t => t.IdUsuarioInclusão)
                .Include(t => t.IdUsuarioAtualizacao)
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
                novoTipoRequerimento.DataInclusão = DateTime.Now;
                novoTipoRequerimento.IdUsuarioInclusão = GetUsuarioLogadoId();
                novoTipoRequerimento.DataAlteracao = DateTime.Now;
                novoTipoRequerimento.IdUsuarioAtualizacao = GetUsuarioLogadoId();

                _context.TiposRequerimento.Add(novoTipoRequerimento);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetTipoRequerimentoById), new { id = novoTipoRequerimento.Id }, novoTipoRequerimento);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar tipo de requerimento: {ex.Message}");
            }
        }

        private int GetUsuarioLogadoId()
        {
            //Falta implementar melhor o metodo pois ele prenche o user "1"
            return 1;
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

        // ENDPOINT excluirLogico....



    }
}