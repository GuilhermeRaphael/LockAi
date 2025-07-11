using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Models.Enuns;

namespace LockAi.Models
{
    public class PlanoLocacao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DtInicio { get; set; }
        public DateTime DtFim { get; set; }
        public float Valor { get; set; }
        public string InicioLocacao { get; set; }
        public string FimLocacao { get; set; }
        public int PrazoPagamento { get; set; }
        public SituacaoPlanoLocacao Situacao { get; set; }
        public DateTime DtInclusao { get; set; }
        public int IdUsuarioInclusao { get; set; }
        public DateTime DtAtualizacao { get; set; }
        public int IdUsuarioAtualizacao { get; set; }
        
    }
}