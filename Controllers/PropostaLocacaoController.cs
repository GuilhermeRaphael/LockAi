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

       [HttpPost]
        public async Task<IActionResult> CriarProposta([FromBody] PropostaLocacao propostaDto)
        {
            try
            {
                var plano = await _context.PlanosLocacao
                    .FirstOrDefaultAsync(p => p.Id == propostaDto.IdPlanoLocacao);

                if (plano == null)
                    return NotFound("Plano de locação não encontrado.");

                if (plano.Situacao == SituacaoPlanoLocacao.Inativo || plano.Situacao == SituacaoPlanoLocacao.Pendente)
                    return BadRequest("Este plano não pode ser utilizado.");

                propostaDto.Valor = plano.Valor;
                propostaDto.DtInicio = plano.DtInicio;
                propostaDto.DtFim = plano.DtFim;

                propostaDto.Data = DateTime.UtcNow;
                propostaDto.Situacao = SituacaoPropostaEnum.EmAnalise;
                propostaDto.DtSituacao = DateTime.UtcNow;

                var objeto = await _context.Objetos.FindAsync(propostaDto.IdObjeto);
                if (objeto == null)
                    return NotFound("Objeto não encontrado.");

                objeto.Situacao = SituacaoObjetoEnum.Reservado;
                objeto.DtAtualizao = DateTime.UtcNow;
                objeto.IdUsuarioAtualizacao = propostaDto.IdUsuario;

                _context.PropostaLocacao.Add(propostaDto);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    Message = "Proposta criada com sucesso.",
                    Proposta = propostaDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar proposta: {ex.Message}");
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


        

        
    }
}