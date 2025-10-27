using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Data;
using LockAi.Models;
using Microsoft.AspNetCore.Mvc;

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
        
         private void ValidarLocacao(PropostaLocacao locacao)
        {
            if (locacao == null)
                throw new ArgumentException("A proposta de locação não pode ser nula.");

            if (locacao.DtInicio >= locacao.DtFim)
                throw new ArgumentException("A data de início deve ser anterior à data de fim.");

            if (locacao.Valor <= 0)
                throw new ArgumentException("O valor deve ser maior que zero.");
        }


    }
}