using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Models.Enuns;

namespace LockAi.Models
{
    public class Objeto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string LocalidadePrimaria { get; set; }
        public string LocalidadeSecundaria { get; set; }
        public string LocalidadeTercearia { get; set; }
        public SituacaoObjetoEnum Situacao { get; set; }
        public int IdTipoObjeto { get; set; } // FK
        public TipoObjeto? TipoObjeto { get; set; }
        public DateTime DtInclusao { get; set; }
        public int IdUsuarioInclusao { get; set; }
        public DateTime DtAtualizao { get; set; }
        public int IdUsuarioAtualizacao { get; set; }

        public ICollection<PropostaLocacao>? PropostaLocacao { get; set; }
    }
}