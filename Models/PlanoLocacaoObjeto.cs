using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Models.Enuns;

namespace LockAi.Models
{
    public class PlanoLocacaoObjeto
    {
        // public int Id { get; set; }
        public int IdPlanoLocacao { get; set; }//chave estrangeira
        public PlanoLocacao? PlanoLocacao { get; set; } //Navegação
        public int IdTipoObjeto { get; set; } //FK
        public TipoObjeto? TipoObjeto { get; set; } //Navegação
        public SituacaoPlanoLocacaoObjeto Situacao { get; set; }
        public DateTime DtInclusao { get; set; }
        public int IdUsuarioInclusao { get; set; }
        public DateTime DtAtualizacao { get; set; }
        public int IdUsuarioAtualizacao { get; set; }


    }
}