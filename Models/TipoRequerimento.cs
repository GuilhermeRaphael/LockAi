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
        public DateTime DataInclusao { get; set; }
        public int IdUsuarioInclusao { get; set; } // FK
        public Usuario? UsuarioInclusao { get; set; } // navegação 
        public DateTime DataAlteracao { get; set; }
        public int IdUsuarioAtualizacao { get; set; } // FK
        public Usuario? UsuarioAtualizacao { get; set; } // navegação 
        public ICollection<Requerimento>? Requerimentos { get; set; }
    }
}