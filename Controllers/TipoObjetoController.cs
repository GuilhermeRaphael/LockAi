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
    public class TipoObjetoController : ControllerBase
    {
        private readonly DataContext _context;

        public TipoObjetoController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> ConsultarTipoObjeto()
        {
            var tipoObjeto = await _context.TipoObjeto.ToListAsync();
            return Ok(tipoObjeto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorIdTipoObjeto(int id)
        {
            try
            {
                TipoObjeto tipoObjeto = await _context.TipoObjeto.FirstOrDefaultAsync(o => o.Id == id);

                if (tipoObjeto == null)
                {
                    return NotFound("Tipo objeto não encontrado");
                }
                return Ok(tipoObjeto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar Tipo objeto {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostTipoObjeto(TipoObjeto novoTipoObjeto)
        {
            try
            {
                _context.TipoObjeto.Add(novoTipoObjeto);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPorIdTipoObjeto), new { id = novoTipoObjeto.Id }, novoTipoObjeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoObjeto(int id)
        {
            try
            {
                var tipoObjeto = await _context.TipoObjeto.FindAsync(id);
                if (tipoObjeto == null)
                {
                    return NotFound($"Objeto com ID {id} não encontrado.");
                }
                _context.TipoObjeto.Remove(tipoObjeto);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir tipo objeto; {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoObjeto(int id)
        {
            try
            {
                var tipoObjeto = await _context.TipoObjeto.FindAsync(id);

                if (tipoObjeto == null)
                {
                    return NotFound("Tipo objeto não foi encontrado");
                }

                _context.TipoObjeto.Update(tipoObjeto);
                await _context.SaveChangesAsync();

                return Ok(tipoObjeto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar o tipo objeto: {ex.Message}");
            }
        }





    }
}