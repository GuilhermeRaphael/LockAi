using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Models.Enuns;

namespace LockAi.Models
{
    public class Locacao
    {
        public int Id { get; set; }
        public int IdPropostaLocacao { get; set; }
        public PropostaLocacao propostaLocacao { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public float Valor { get; set; }
        public SituacaoLocacaoEnum Situacao { get; set; }
        public string DataSituacao { get; set; }
        public int IdUsuarioSituacao { get; set; }
    
     
    }
}