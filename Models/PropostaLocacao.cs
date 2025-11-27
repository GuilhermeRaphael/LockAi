using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Models.Enuns;


namespace LockAi.Models
{
    public class PropostaLocacao
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int IdUsuario { get; set; } //FK
        public Usuario? Usuario { get; set; } // Navegação
        public int IdObjeto { get; set; } // FK
        public Objeto? Objeto { get; set; } // Navegação
        public int IdPlanoLocacao { get; set; } // FK
        public PlanoLocacao? PlanoLocacao { get; set; } // Navegação
        public DateTime DtInicio { get; set; }
        public DateTime DtFim { get; set; }
        public DateTime DtValidade { get; set; }
        public float Valor { get; set; }
        public SituacaoPropostaEnum Situacao { get; set; }
        public DateTime DtSituacao { get; set; }
        public int IdUsuarioSituacao { get; set; }
        public Locacao? Locacao { get; set; } // Navegação
        public PropostaLocacaoPagamento? Pagamento { get; set; }


        // nescessaio ver e analisar a logica de realcionamento
    }
}