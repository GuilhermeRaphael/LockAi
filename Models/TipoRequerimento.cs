using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Models.Enuns;

namespace LockAi.Models
{
    public class TipoRequerimento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public float Valor { get; set; }
        public SituacaoTipoRequerimentoEnum Situacao { get; set; }
        public DateTime DataInclusão { get; set; }
        public int IdUsuarioInclusão { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int IdUsuarioAtualizacao { get; set; }
    }
}