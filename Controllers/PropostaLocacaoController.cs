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
    public class PropostaLocacaoController : ControllerBase
    {
        private readonly DataContext _context;

        public PropostaLocacaoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPropostaLocacao()
        {
            try
            {
                var lista = await _context.PropostaLocacao
                .Include(p => p.Usuario)
                .Include(p => p.PlanoLocacao)
                .Include(p => p.Objeto)
                .ToListAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter propostas de locação: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropostaLocacaoById(int id)
        {
            try
            {
                var proposta = await _context.PropostaLocacao
                .Include(p => p.Usuario)
                .Include(p => p.PlanoLocacao)
                .Include(p => p.Objeto)
                .FirstOrDefaultAsync(p => p.Id == id);

                if (proposta == null)
                    return NotFound("Proposta de locação não encontrada.");

                return Ok(proposta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter proposta de locação: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostPropostaLocacao(PropostaLocacao novaProposta)
        {
            try
            {
                if (novaProposta == null)
                    return BadRequest("Dados da proposta de locação são obrigatórios.");

                novaProposta.Situacao = SituacaoPropostaEnum.EmAnalise;

                _context.PropostaLocacao.Add(novaProposta);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPropostaLocacaoById), new { id = novaProposta.Id }, novaProposta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar proposta de locação: {ex.Message}");
            }
        }

        [HttpPatch("{id}/cancelar")]
        public async Task<IActionResult> CancelarPropostaLocacao(int id)
        {
            try
            {
                var proposta = await _context.PropostaLocacao.FindAsync(id);

                if (proposta == null)
                    return NotFound("Proposta de locação não encontrada.");

                if (proposta.Situacao == SituacaoPropostaEnum.Cancelada)
                    return BadRequest("A proposta de locação já está cancelada.");

                if (proposta.Situacao == SituacaoPropostaEnum.Aprovada)
                    return BadRequest("A proposta de locação aprovada não pode ser cancelada.");

                proposta.Situacao = SituacaoPropostaEnum.Cancelada;
                proposta.DtSituacao = DateTime.UtcNow;

                _context.PropostaLocacao.Update(proposta);
                await _context.SaveChangesAsync();

                return Ok("Proposta locação cancelada com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao cancelar proposta de locação: {ex.Message}");
            }
        }

        [HttpPut("aprovar/{id}")]
        public async Task<IActionResult> AprovarPropostaLocacao(int id)
        {
            try
            {
                var proposta = await _context.PropostaLocacao
                .Include(p => p.Objeto)
                .FirstOrDefaultAsync(p => p.Id == id);


                    if (proposta == null)
                        return NotFound("Proposta não encontrada.");

                    if (proposta.Situacao == SituacaoPropostaEnum.Aprovada)
                        return BadRequest("Essa proposta já foi aprovada.");

                    
                    proposta.Situacao = SituacaoPropostaEnum.Aprovada;
                    var locacao = new Locacao
                    {
                        IdPropostaLocacao = proposta.Id,      
                        IdUsuario = proposta.IdUsuario,       
                        DataInicio = DateTime.UtcNow,         
                        DataFim = DateTime.UtcNow.AddMonths(1),
                        Valor = proposta.Valor,               
                        Situacao = SituacaoLocacaoEnum.Ativa, 
                        DataSituacao = DateTime.UtcNow,
                        IdUsuarioSituacao = proposta.IdUsuario 
                    };

                    _context.Locacoes.Add(locacao);

                    proposta.Objeto.Situacao = SituacaoObjetoEnum.Locado;
                    proposta.Objeto.DtAtualizao = DateTime.Now;
                    proposta.Objeto.IdUsuarioAtualizacao = proposta.IdUsuario;

                    await _context.SaveChangesAsync();

                    return Ok(new 
                    {
                        Message = "Proposta aprovada e locação gerada automaticamente.",
                        LocacaoGerada = locacao
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao aprovar proposta de locação: {ex.Message}");                
            }
        }


        

        
    }
}