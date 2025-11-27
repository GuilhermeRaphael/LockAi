using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LockAi.Models.Enuns;

namespace LockAi.Models
{
    public class Locacao
    {
        public int Id { get; set; }
        public int IdPropostaLocacao { get; set; } // FK
        public PropostaLocacao PropostaLocacao { get; set; } // Navegação
        public int IdUsuario { get; set; }  // FK
        public Usuario? Usuario { get; set; } // Navegação
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public float Valor { get; set; }
        public SituacaoLocacaoEnum Situacao { get; set; }
        public DateTime DataSituacao { get; set; }
        public int IdUsuarioSituacao { get; set; }
        // 1:1
        [JsonIgnore]
        public LocacaoParceiro? LocacaoParceiro { get; set; }
        public ICollection<Requerimento>? Requerimento { get; set; }
        

    }
}