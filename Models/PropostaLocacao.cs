using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using LockAi.Models.Enuns;
namespace LockAi.Models
{
    public class PropostaLocacao
    {
        public int Id { get; set; }
        public int PropostaLocacaoId { get; set; }
        public DateTime Data { get; set; }
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
        public int IdObjeto { get; set; }
        public Objeto? Objeto { get; set; }
        public int PlanoLocacaoId { get; set; } 

        [ForeignKey("PlanoLocacaoId")] 
        public PlanoLocacao? PlanoLocacao { get; set; }
        public DateTime DtInicio { get; set; }
        public DateTime DtFim { get; set; }
        public DateTime DtValidade { get; set; }
        public float Valor { get; set; }
        public SituacaoPropostaEnum Situacao { get; set; }
        public DateTime DtSituacao { get; set; }
        public int IdUsuarioSituacao { get; set; }
    }
}