using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Models.Enuns;

namespace LockAi.Models
{
    public class PropostaLocacaoPagamento
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Comprovante { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DtConferencia { get; set; }
        public int IdUsuarioConferencia { get; set; }
        public SituacaoPropostaLocacaoPagamento Situacao { get; set; }
        public int IdPropostaLocacao { get; set; }
        public PropostaLocacao PropostaLocacao { get; set; }
    }
}