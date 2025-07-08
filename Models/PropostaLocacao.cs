using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LockAi.Models.Enuns
{
    public class PropostaLocacao
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public int IdObjeto { get; set; }
        public Objeto Objeto { get; set; }
        public int IdPlanoLocacao { get; set; }
        public PlanoLocacao PlanoLocacao { get; set; }
        public DateTime DtInicio { get; set; }
        public DateTime DtFim { get; set; }
        public DateTime DtValidade { get; set; }
        public float Valor { get; set; }
        public SituacaoPropostaEnum Situacao { get; set; }
        public DateTime DtSituacao { get; set; }
        public int IdUsuarioSituacao { get; set; }
    }
}