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
        public SituacaoTipoObjetoEnum Situacao { get; set; }
        public DateTime DtInclusao { get; set; }
        public int IdUsuarioInclusao { get; set; } //FK
        public Usuario? UsuarioInclusao { get; set; } //naveção
        public DateTime DtAtualizacao { get; set; }
        public int IdUsuarioAtualizacao { get; set; } // Fk
        public Usuario? UsuarioAtualizacao { get; set; } //navegação
        public ICollection<PlanoLocacaoObjeto>? PlanoLocacaoObjetos { get; set; }
        public ICollection<Objeto>? Objeto { get; set; }
    }
}