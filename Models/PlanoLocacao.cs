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
        public int IdUsuarioInclusao { get; set; } // FK
        public Usuario? UsuarioInclusao { get; set; } // Navegação
        public DateTime DtAtualizacao { get; set; }
        public int IdUsuarioAtualizacao { get; set; } // FK
        public Usuario? UsuarioAtualizacao { get; set; } // Navegacao
        public int UsuarioId { get; set; } //chave estrangeira
        public Usuario? Usuario { get; set; } // Navegação
        public ICollection<PlanoLocacaoObjeto>? PlanoLocacaoObjetos { get; set; }
        public ICollection<PropostaLocacao>? PropostaLocacao { get; set; }

    }
}