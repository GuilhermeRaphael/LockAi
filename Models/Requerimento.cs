using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Models.Enuns;

namespace LockAi.Models
{
    public class Requerimento
    {
        public Usuario IdUsuario { get; set; }
        public int Id { get; set; }
        public DateTime Momento { get; set; }
        public int IdTipoRequerimento { get; set; }

        //Realizar Relacionamento
        public int IdLocacao { get; set; }
        public string Observacao { get; set; }
        public SituacaoRequerimentoEnum Situacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int IdUsuarioAtualizacao { get; set; }
    }
}