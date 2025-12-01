using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Data;
using LockAi.Models;
using LockAi.Models.Enuns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LockAi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ObjetoController : ControllerBase
    {
        private DataContext _context;

        public ObjetoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarObjetos()
        {
            var Objetos = await _context.Objetos.ToListAsync();
            return Ok(Objetos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ConsultarObjetoPorId(int id)
        {
            try
            {
                var objeto = await _context.Objetos.FirstOrDefaultAsync(o => o.Id == id);

                if (objeto == null)
                {
                    return NotFound();
                }

                return Ok(objeto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar obejto: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AdicionarObjeto(Objeto novoObjeto)
        {
            try
            {
                _context.Objetos.Add(novoObjeto);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(ConsultarObjetoPorId), new { id = novoObjeto.Id }, novoObjeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirObjeto(int id)
        {
            try
            {
                var objeto = await _context.Objetos.FindAsync(id);
                if (objeto == null)
                {
                    return NotFound($"Objeto com ID {id} não encontrado.");
                }
                _context.Objetos.Remove(objeto);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir objeto; {ex.Message}");
            }
        }

        [HttpPut("reservar/{id}")]
        public async Task<IActionResult> ReservarObjeto(int id)
        {
            try
            {
                var objeto = await _context.Objetos.FindAsync(id);
                if (objeto == null)
                {
                    return NotFound($"Objeto com ID {id} não encontrado.");
                }
                if (objeto.Situacao == SituacaoObjetoEnum.Reservado || objeto.Situacao == SituacaoObjetoEnum.Locado)
                {
                    return BadRequest("O objeto não pode ser RESERVADO!! O objeto ja esta locado ou reservado");
                }

                objeto.Situacao = SituacaoObjetoEnum.Reservado;
                _context.Objetos.Update(objeto);
                await _context.SaveChangesAsync();

                return Ok(objeto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao reservar objeto : {ex.Message}");
            }
        }


        [HttpPut("liberar/{id}")]
        public async Task<IActionResult> LiberarObjeto(int id)
        {
            try
            {
                var objeto = await _context.Objetos.FindAsync(id);

                if (objeto == null)
                {
                    return BadRequest($"O objeto com ID {id} não encontrado");
                }

                if (objeto.Situacao == SituacaoObjetoEnum.Locado || objeto.Situacao == SituacaoObjetoEnum.Ativo)
                {
                    return BadRequest("O objeto não pode ser liberado! Pois ja esta ativo ou locado por outra pessoa!");
                }

                objeto.Situacao = SituacaoObjetoEnum.Ativo;
                _context.Objetos.Update(objeto);
                await _context.SaveChangesAsync();

                return Ok($"Objeto com ID {id} foi liberado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao liberar objeto: {ex.Message}");
            }
        }

        [HttpPut("bloquear/{id}")]
        public async Task<IActionResult> DesativarObjeto(int id)
        {
            try
            {
                var objeto = await _context.Objetos.FindAsync(id);

                if (objeto == null)
                {
                    return BadRequest($"O objeto com ID {id} não encontrado");
                }

                if (objeto.Situacao == SituacaoObjetoEnum.Desativado)
                {
                    return BadRequest($"O objeto com ID {id} ja esta desativado!");
                }

                objeto.Situacao = SituacaoObjetoEnum.Desativado;
                _context.Objetos.Update(objeto);
                await _context.SaveChangesAsync();

                return Ok($"Objeto com ID {id} foi desativado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao desativar objeto: {ex.Message}");
            }
        }

        [HttpPost("teste")]
        public async Task<IActionResult> CriarObjetoTeste()
        {
            try
            {
                var objetoTeste = new Objeto
                {
                    Nome = "Cadeado Inteligente",
                    Descricao = "Objeto de teste para validação de modelo",
                    LocalidadePrimaria = "São Paulo",
                    LocalidadeSecundaria = "Zona Sul",
                    LocalidadeTercearia = "Bloco A",
                    Situacao = SituacaoObjetoEnum.Ativo,
                    IdTipoObjeto = 1, // Supondo que esse tipo exista
                    DtInclusao = DateTime.Now,
                    IdUsuarioInclusao = 1, // Supondo que esse usuário exista
                    DtAtualizao = DateTime.Now,
                    IdUsuarioAtualizacao = 1
                };

                _context.Objetos.Add(objetoTeste);
                await _context.SaveChangesAsync();

                return Ok(objetoTeste);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar objeto de teste: {ex.Message}");
            }
        }
    }
}