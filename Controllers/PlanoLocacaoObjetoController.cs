using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Data;
using LockAi.Models;
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
        public async Task<IActionResult> GetId([FromBody] int idPlano)
        {
            try
            {
                PlanoLocacaoObjeto planoLocacaoObjeto = await _context.PlanosLocacoesObjeto.FirstOrDefaultAsync(r => r.IdPlanoLocacao == idPlano);

                if (planoLocacaoObjeto == null)
                    return NotFound("Plano Locação de objeto não encontrado.");

                return Ok(planoLocacaoObjeto);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 

        
    }
}