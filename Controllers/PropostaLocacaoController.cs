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

        [HttpPost("ValidarLocacao")]
        public async Task<IActionResult> AddPropostaLocacao(PropostaLocacao novaLocacao)
        {
            try
            {
                ValidarLocacao(novaLocacao);

                _context.PropostasLocacao.Add(novaLocacao);
                await _context.SaveChangesAsync();

                return CreatedAtRoute("GetPropostaLocacaoById",
                    new { id = novaLocacao.Id }, novaLocacao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProposta(int id, PropostaLocacao propostaAtualizada)
        {
            var proposta = await _context.PropostasLocacao.FindAsync(id);

            if (proposta == null)
            {
                return NotFound("Proposta não encontrada.");
            }
            proposta.Valor = propostaAtualizada.Valor;
            proposta.Situacao = propostaAtualizada.Situacao;

            await _context.SaveChangesAsync();

            return Ok(proposta); // Retorna a proposta com os dados atualizados
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var propostas = await _context.PropostasLocacao.ToListAsync();
            return Ok(propostas);
        }

        [HttpGet("{id:int}", Name = "GetPropostaLocacaoById")]
        public async Task<IActionResult> GetPropostaLocacaoById(int id)
        {
            var proposta = await _context.PropostasLocacao.FindAsync(id);
            if (proposta == null)
                return NotFound();
            return Ok(proposta);
        }

        private void ValidarLocacao(PropostaLocacao locacao)
        {
            if (locacao == null)
                throw new ArgumentException("A proposta de locação não pode ser nula.");

            if (locacao.DtInicio >= locacao.DtFim)
                throw new ArgumentException("A data de início deve ser anterior à data de fim.");

            if (locacao.Valor <= 0)
                throw new ArgumentException("O valor deve ser maior que zero.");
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProposta(int id)
        {
        var proposta = await _context.PropostasLocacao.FindAsync(id);

        if (proposta == null)
        {
        return NotFound("Proposta não encontrada.");
        }

         _context.PropostasLocacao.Remove(proposta);
         await _context.SaveChangesAsync();

         // Retorna uma mensagem de sucesso
         return Ok("Proposta de ID " + id + " foi deletada com sucesso."); 
        }
    }
}
