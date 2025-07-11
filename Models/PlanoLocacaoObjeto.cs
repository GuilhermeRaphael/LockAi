using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Models.Enuns;

namespace LockAi.Models
{
    public class PlanoLocacaoObjeto
    {
        public int IdPlanoLocacao { get; set; }
        public PlanoLocacao PlanoLocacao { get; set; }
        public int IdTipoObjeto { get; set; }
        public TipoObjeto TipoObjeto { get; set; }
        public SituacaoPlanoLocacaoObjeto Situacao { get; set; }
        public DateTime DtInclusao { get; set; }
        public int IdUsuarioInclusao { get; set; }
        public DateTime DtAtualizacao { get; set; }
        public int IdUsuarioAtualizacao { get; set; }
    }
}