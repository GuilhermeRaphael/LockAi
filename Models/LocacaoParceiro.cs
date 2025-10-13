using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Models.Enuns;

namespace LockAi.Models
{
    public class LocacaoParceiro
    {
        public int Id { get; set; }
        public int? IdLocacao { get; set; } // FK
        public Locacao? Locacao { get; set; } // navegação
        public int IdParceiro { get; set; } 
        public string IdentificacaoParceiro { get; set; }
        public string NomeParceiro { get; set; }
        public SituacaoLocacaoParceiroEnum Situacao { get; set; }
        public DateTime DtSituacao { get; set; }
        public int IdUsuarioSituacao { get; set; }
    }
}