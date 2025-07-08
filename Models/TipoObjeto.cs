using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Models.Enuns;

namespace LockAi.Models
{
    public class TipoObjeto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public SituacaoEnum situacaoEnum { get; set; }
        public DateTime DtInclusao { get; set; }
        public int IdUsuarioInclusao { get; set; }
        public DateTime DtAtualizao { get; set; }
        public int IdUsuarioAtualizacao { get; set; }
    }
}