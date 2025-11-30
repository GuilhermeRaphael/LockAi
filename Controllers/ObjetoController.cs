using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LockAi.Data;
using LockAi.Models;
using LockAi.Models.Enuns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LockAi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ObjetoController : ControllerBase
    {
        private DataContext _context;

        public ObjetoController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ConsultarObjetos()
        {
            try
            {
                var lista = await _context.Objetos.ToListAsync();
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro ao buscar Objetos: {ex.Message}");
            }
        }

        [Authorize]
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

        [Authorize(Policy = "Gestor")]
        [HttpPost]
        public async Task<IActionResult> AdicionarObjeto(Objeto novoObjeto)
        {
            try
            {
                var usuario = await GetUsuarioLogadoAsync();
                
                if (usuario == null)
                return Unauthorized("Usuário não identificado.");

                novoObjeto.IdUsuarioInclusao = usuario.Id;
                novoObjeto.IdUsuarioAtualizacao = usuario.Id;
                ValidarPosicao(novoObjeto.PosicaoArmario);    

                _context.Objetos.Add(novoObjeto);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(ConsultarObjetoPorId), new { id = novoObjeto.Id }, novoObjeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "Gestor")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirObjeto(int id)
        {
            try
            {
                var usuario = await GetUsuarioLogadoAsync();

                if (usuario == null)
                return Unauthorized("Usuário não identificado.");

                var objeto = await _context.Objetos.FindAsync(id);
                if (objeto == null)
                {
                    return NotFound($"Objeto com ID {id} não encontrado.");
                }

                objeto.IdUsuarioAtualizacao = usuario.Id;

                _context.Objetos.Remove(objeto);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir objeto; {ex.Message}");
            }
        }

        [Authorize]
        [HttpPut("reservar/{id}")]
        public async Task<IActionResult> ReservarObjeto(int id)
        {
            try
            {
                var usuario = await GetUsuarioLogadoAsync();
                
                if (usuario == null)
                return Unauthorized("Usuário não identificado.");

                var objeto = await _context.Objetos.FindAsync(id);
                if (objeto == null)
                {
                    return NotFound($"Objeto com ID {id} não encontrado.");
                }
                if (objeto.Situacao == SituacaoObjetoEnum.Reservado || objeto.Situacao == SituacaoObjetoEnum.Locado)
                {
                    return BadRequest("O objeto não pode ser RESERVADO!! O objeto ja esta locado ou reservado");
                }

                objeto.IdUsuarioAtualizacao = usuario.Id;
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

        [Authorize(Policy = "Gestor")]
        [HttpPut("liberar/{id}")]
        public async Task<IActionResult> LiberarObjeto(int id)
        {
            try
            {
                var usuario = await GetUsuarioLogadoAsync();
                if (usuario == null)
                {
                    return Unauthorized("Usuário não identificado.");
                }

                var objeto = await _context.Objetos.FindAsync(id);
                if (objeto == null)
                {
                    return BadRequest($"O objeto com ID {id} não encontrado");
                }

                if (objeto.Situacao == SituacaoObjetoEnum.Locado || objeto.Situacao == SituacaoObjetoEnum.Ativo)
                {
                    return BadRequest("O objeto não pode ser liberado! Pois ja esta ativo ou locado por outra pessoa!");
                }

                objeto.IdUsuarioAtualizacao = usuario.Id;
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

        [Authorize(Policy = "Gestor")]
        [HttpPut("bloquear/{id}")]
        public async Task<IActionResult> DesativarObjeto(int id)
        {
            try
            {
                var usuario = await GetUsuarioLogadoAsync();
                if (usuario == null)
                return Unauthorized("Usuário não identificado.");

                var objeto = await _context.Objetos.FindAsync(id);
                if (objeto == null)
                {
                    return NotFound($"O objeto com ID {id} não encontrado");
                }

                if (objeto.Situacao == SituacaoObjetoEnum.Desativado)
                {
                    return BadRequest($"O objeto com ID {id} ja esta desativado!");
                }

                objeto.IdUsuarioAtualizacao = usuario.Id;
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

        public void ValidarPosicao(string posicao)
        {
            if (string.IsNullOrWhiteSpace(posicao))
                throw new Exception("A posição é obrigatória. Use: alto, medio ou baixo.");

            var validas = new[] { "alto", "medio", "baixo" };

            if (!validas.Contains(posicao.ToLower()))
            throw new Exception("Posição inválida. Use: alto, medio ou baixo.");
        }

         private async Task<Usuario> GetUsuarioLogadoAsync()
        {
            var userIdClaim =  User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return null;

            int userId = int.Parse(userIdClaim.Value);

            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == userId);
        }

    }
}