using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Models.Enuns;

namespace LockAi.Models
{
    public class Requerimento
    {
        public int Id { get; set; }
        public DateTime Momento { get; set; }
        public int IdLocacao { get; set; }
        public Locacao? Locacao { get; set; }
        public string Observacao { get; set; }
        public SituacaoRequerimentoEnum Situacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int IdUsuarioAtualizacao { get; set; } // utilizar a mesma sintxe de TipRequerimento

        public int UsuarioId { get; set; } // chave estrangeira correta
        public Usuario? Usuario { get; set; } // navegação
        public int TipoRequerimentoId { get; set; }
        public TipoRequerimento? TipoRequerimento { get; set; } //navegação
    }
 }
