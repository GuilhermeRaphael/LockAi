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
    public class RequerimentoController : ControllerBase
    {
        private readonly DataContext _context;

        public RequerimentoController(DataContext context)
        {
            _context = context;
        }

        // EndPoint para listar todos, e mostrar o usuario que solicitou.
        [HttpGet("GetlAll")]
        public async Task<IActionResult> GetRequerimentos()
        {
            try
            {
                List<Requerimento> lista = await _context.Requerimentos.Include(r => r.Usuario).ToListAsync();

                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Consultar por ID
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetRequerimentoId(int id)
        {
            try
            {
                Requerimento requerimentos = await _context.Requerimentos.Include(r => r.Usuario).FirstOrDefaultAsync(rBusca => rBusca.Id == id);

                if (requerimentos == null)
                    return NotFound("Requerimento não encontrado.");
                return Ok(requerimentos);

            }
            catch (System.Exception ex)
            {
                return BadRequest($"Erro ao buscar o requerimento: {ex.Message}");
            }
        }

        /*
        [HttpGet("{Situacao}")]
        public async Task<IActionResult> GetSituacaoRequerimento(int SituacaoRequerimentoEnum)
        {

        }
        */


    }
}