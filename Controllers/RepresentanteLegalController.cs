using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LockAi.Data;
using LockAi.Models;
using Microsoft.EntityFrameworkCore;
using LockAi.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace LockAi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
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
                    .Include(r => r.Usuarios)
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

        [HttpPatch("{id}/email")]
        public async Task<IActionResult> AlterarEmail(int id, [FromBody] AlterarEmailDto dto)
        {
            try
            {
                var representante = await _context.RepresentanteLegal
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (representante == null)
                    return NotFound("Representante legal não encontrado.");

                representante.Email = dto.Email; //Atualiza o email antigo com o novo enviado

                await _context.SaveChangesAsync();

                return Ok(representante);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar email: {ex.Message}");
            }
            // DTO (Data Transfer Object)
            // Usado para receber ou enviar apenas os dados necessários entre o frontend e a API.
            // Isso evita expor ou alterar todo o objeto da model (RepresentanteLegal) acidentalmente.
            // Neste caso, usamos o DTO para alterar apenas o email do representante legal.
        }

            [HttpPatch("{id}/telefone")]
            public async Task<IActionResult> AlterarTelefone(int id, [FromBody] AlterarTelefoneDto dto)
            {
                try
                {
                    var representante = await _context.RepresentanteLegal
                        .FirstOrDefaultAsync(r => r.Id == id);

                    if (representante == null)
                        return NotFound("Representante legal não encontrado.");

                    representante.Telefone = dto.Telefone;

                    await _context.SaveChangesAsync();

                    return Ok(representante);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao alterar telefone: {ex.Message}");
                }
            }

        public void ValidarRepresentante(RepresentanteLegal representanteLegal)
        {
            if (string.IsNullOrWhiteSpace(representanteLegal.Nome))
                throw new Exception("Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(representanteLegal.Cpf) || representanteLegal.Cpf.Length != 11) //se CPF for diferente á 11 digitos
                throw new Exception("CPF inválido.");

            if (string.IsNullOrWhiteSpace(representanteLegal.Telefone))
                throw new Exception("Telefone é obrigatório.");

        }



    } // fim da classe do tipo controller
}