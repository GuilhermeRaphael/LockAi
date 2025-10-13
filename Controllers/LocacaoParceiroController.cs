using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Data;
using LockAi.Models;
using LockAi.Models.Enuns;
using Microsoft.AspNetCore.Mvc;

namespace LockAi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocacaoParceiroController :ControllerBase
    {
         private DataContext _context;

        public LocacaoParceiroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParceiroById (int id)
        {
            //Continuar . . .
        }

        [HttpPost]
        public async Task<IActionResult> AddParceiro(LocacaoParceiro locacaoParceiro)
        {
            try
            {

                locacaoParceiro.Situacao = SituacaoLocacaoParceiroEnum.Pendente;
                locacaoParceiro.DtSituacao = DateTime.Now;
                var usuario = await GetUsuarioLogadoAsync();
                locacaoParceiro.IdUsuarioSituacao = usuario.Id;

                _context.LocacoesParceiro.Add(locacaoParceiro);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetParceiroById), new { id = locacaoParceiro.Id }, locacaoParceiro);


            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar parceiro. {ex.Message}");
            }

        }
        
        private async Task<Usuario> GetUsuarioLogadoAsync()
        {
            return await _context.Usuarios.FindAsync(1); // ID fixo por enquanto, mudar com a implementação do JWT
        }



    }
}