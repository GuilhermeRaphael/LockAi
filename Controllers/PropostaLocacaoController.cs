using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Data;
using LockAi.Dtos;
using LockAi.Models;
using LockAi.Models.Enuns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace LockAi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PropostaLocacaoController : ControllerBase
    {
        private readonly DataContext _context;

        public PropostaLocacaoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPropostaLocacao()
        {
            try
            {
                var lista = await _context.PropostaLocacao
                .Include(p => p.Usuario)
                .Include(p => p.PlanoLocacao)
                .Include(p => p.Objeto)
                .ToListAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter propostas de locação: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropostaLocacaoById(int id)
        {
            try
            {
                var proposta = await _context.PropostaLocacao
                .Include(p => p.Usuario)
                .Include(p => p.PlanoLocacao)
                .Include(p => p.Objeto)
                .FirstOrDefaultAsync(p => p.Id == id);

                if (proposta == null)
                    return NotFound("Proposta de locação não encontrada.");

                return Ok(proposta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter proposta de locação: {ex.Message}");
            }
        }

       [Authorize(Policy = "Usuario")]
       [HttpPost]
        public async Task<IActionResult> CriarProposta([FromBody] EnviarPropostaDto dto)
        {
            try
            {
                var usuarioLogado = await GetUsuarioLogadoAsync();
                 if (usuarioLogado == null)
                    return Unauthorized(new { mensagem = "Usuário não autenticado." });

                var objeto = await _context.Objetos.FindAsync(dto.IdObjeto);

                if (objeto == null)
                    return BadRequest(new { mensagem = "Objeto não encontrado." });

                if (objeto.Situacao == SituacaoObjetoEnum.Reservado)
                    return BadRequest(new { mensagem = "Objeto já está reservado." });

                if (objeto.Situacao == SituacaoObjetoEnum.Locado)
                    return BadRequest(new { mensagem = "Objeto já está locado." });

                objeto.Situacao = SituacaoObjetoEnum.Reservado;
                _context.Objetos.Update(objeto);

                var proposta = new PropostaLocacao
                {
                    IdObjeto = dto.IdObjeto,
                    IdPlanoLocacao = dto.IdPlanoLocacao,
                    Data = DateTime.Now,
                    DtInicio = DateTime.Now,
                    DtFim = DateTime.Now.AddDays(30),
                    DtValidade = DateTime.Now.AddDays(3),
                    Situacao = SituacaoPropostaEnum.AguardandoPagamento,
                    DtSituacao = DateTime.Now,
                    IdUsuario = usuarioLogado.Id,            
                    IdUsuarioSituacao = usuarioLogado.Id,
                    Locacao = new Locacao
                    {
                        DataInicio = DateTime.Now,
                        DataFim = DateTime.Now.AddDays(30),
                        Situacao = SituacaoLocacaoEnum.AguardandoPagamento,
                        DataSituacao = DateTime.Now,
                        IdUsuarioSituacao = usuarioLogado.Id,
                        IdUsuario = usuarioLogado.Id,
                        Valor = 1000
                    },
                    Pagamento = new PropostaLocacaoPagamento
                    {
                    Data = new DateTime(2025, 9, 9),
                    IdUsuario = usuarioLogado.Id,
                    DtConferencia = new DateTime(2025, 9, 10),
                    IdUsuarioConferencia = 1,
                    Situacao = SituacaoPropostaLocacaoPagamento.Aprovado,
                    
                    }
                };

              
                
                    _context.PropostaLocacao.Add(proposta);
                    await _context.SaveChangesAsync();
                

                return Ok(new
                {
                    mensagem = "Proposta criada e objeto reservado.",
                    proposta
                });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new
            {
                    message = "Erro ao salvar no banco.",
                    innerMessage = ex.InnerException?.Message,
                    exception = ex.Message,
                    stackTrace = ex.StackTrace
            });
            }   
            catch (Exception ex)
            {
                 var erro = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, new { mensagem = "Erro interno", erro });
            }
        }



        [HttpPatch("{id}/cancelar")]
        public async Task<IActionResult> CancelarPropostaLocacao(int id)
        {
            try
            {
                var proposta = await _context.PropostaLocacao
                .Include(p => p.Objeto)
                .FirstOrDefaultAsync(p => p.Id == id);

                if (proposta == null)
                    return NotFound("Proposta de locação não encontrada.");

                if (proposta.Situacao == SituacaoPropostaEnum.Cancelada)
                    return BadRequest("A proposta de locação já está cancelada.");

                if (proposta.Situacao == SituacaoPropostaEnum.Aprovada)
                    return BadRequest("A proposta de locação aprovada não pode ser cancelada.");

                proposta.Situacao = SituacaoPropostaEnum.Cancelada;
                proposta.DtSituacao = DateTime.UtcNow;

                _context.PropostaLocacao.Update(proposta);

                if (proposta.Objeto != null)
                {
                    proposta.Objeto.Situacao = SituacaoObjetoEnum.Ativo;
                    proposta.Objeto.DtAtualizao = DateTime.UtcNow;
                    proposta.Objeto.IdUsuarioAtualizacao = proposta.IdUsuario;
                    _context.Objetos.Update(proposta.Objeto);
                }

                await _context.SaveChangesAsync();

                return Ok("Proposta locação cancelada com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao cancelar proposta de locação: {ex.Message}");
            }
        }

        [Authorize(Policy = "Gestor")]
        [HttpPut("aprovar/{id}")]
        public async Task<IActionResult> AprovarPropostaLocacao(int id)
        {
            try
            {
                var proposta = await _context.PropostaLocacao
                .Include(p => p.Objeto)
                .FirstOrDefaultAsync(p => p.Id == id);


                    if (proposta == null)
                        return NotFound("Proposta não encontrada.");

                    if (proposta.Situacao == SituacaoPropostaEnum.Aprovada)
                        return BadRequest("Essa proposta já foi aprovada.");
                    
                    proposta.Situacao = SituacaoPropostaEnum.Aprovada;
                    var locacao = new Locacao
                    {
                        IdPropostaLocacao = proposta.Id,      
                        IdUsuario = proposta.IdUsuario,       
                        DataInicio = DateTime.Now,         
                        DataFim = DateTime.Now.AddMonths(1),
                        Valor = proposta.Valor,               
                        Situacao = SituacaoLocacaoEnum.Ativa, 
                        DataSituacao = DateTime.Now,
                        IdUsuarioSituacao = proposta.IdUsuario 
                    };

                    _context.Locacoes.Add(locacao);

                    proposta.Objeto.Situacao = SituacaoObjetoEnum.Locado;
                    proposta.Objeto.DtAtualizao = DateTime.Now;
                    proposta.Objeto.IdUsuarioAtualizacao = proposta.IdUsuario;

                    await _context.SaveChangesAsync();

                    return Ok(new 
                    {
                        Message = "Proposta aprovada e locação gerada automaticamente.",
                        LocacaoGerada = locacao
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao aprovar proposta de locação: {ex.Message}");                
            }
        }

        private async Task<Usuario> GetUsuarioLogadoAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return null;
            }

            if (int.TryParse(userIdClaim.Value, out int userId))
            {
                return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == userId);
            }

            return null;
        }
    }
}
