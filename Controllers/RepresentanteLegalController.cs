using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LockAi.Data;
using LockAi.Models;
using Microsoft.EntityFrameworkCore;

namespace LockAi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RepresentanteLegalController : ControllerBase
    {
        private readonly DataContext _context;

        public RepresentanteLegalController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")] // Busca por ID
        public async Task<IActionResult> GetRepresentanteLegalById(int id)
        {
            try
            {
                RepresentanteLegal representante = await _context.RepresentanteLegal
                    .FirstOrDefaultAsync(rBusca => rBusca.Id == id);

                if (representante == null)
                    return NotFound("Representante legal não encontrado.");

                return Ok(representante);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar representante legal: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRepresentanteLegal(RepresentanteLegal novoRepresentante)
        {
            try
            {
                ValidarRepresentante(novoRepresentante); // Validação antes de salvar

                _context.RepresentanteLegal.Add(novoRepresentante);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetRepresentanteLegalById), new { id = novoRepresentante.Id }, novoRepresentante);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // EM AMDAMENTO
        /*
        [HttpPut("{id}")]
          public async Task<IActionResult> AlterarEmail(int id, [FromBody]UpdateRepresentanteEmailDto dados)
          {
              try
              {
                  RepresentanteLegal representante = await _context.RepresentanteLegal
                      .FirstOrDefaultAsync(r => r.Id == id);

                  if (representante == null)
                      return NotFound("Representante legal não encontrado.");

                  representante.Email = dados.Email;

                  await _context.SaveChangesAsync();

                  return Ok(representante);
              }
              catch (Exception ex)
              {
                  return BadRequest($"Erro ao alterar email: {ex.Message}");
              }
              */

          



        public void ValidarRepresentante(RepresentanteLegal representanteLegal)
        {
            if (string.IsNullOrWhiteSpace(representanteLegal.Nome))
                throw new Exception("Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(representanteLegal.Cpf) || representanteLegal.Cpf.Length != 11) //se CPF ultrapassar os 11 digitos
                throw new Exception("CPF inválido.");

            if (string.IsNullOrWhiteSpace(representanteLegal.Telefone))
                throw new Exception("Telefone é obrigatório.");

            if (representanteLegal.IdUsuario <= 0)
                throw new Exception("ID de usuário inválido.");
        }



    } // fim da classe do tipo controller
}