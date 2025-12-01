using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockAi.Models.Enuns;


namespace LockAi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public DateTime DtNascimento { get; set; }
        public string Telefone { get; set; }
        public int TipoUsuarioId { get; set; }
        public TipoUsuario? TipoUsuario { get; set; }
        public string Senha { get; set; }
        public SituacaoUsuario Situacao { get; set; }
        public DateTime DtSituacao { get; set; }
        public int IdUsuarioSituacao { get; set; }
        public int? RepresentanteLegalId { get; set; } //relacionamento 1:1 com RepresentanteLegal
        public RepresentanteLegal? RepresentanteLegal { get; set; }
        public ICollection<UsuarioImagem>? Imagens { get; set; }
        public ICollection<Requerimento>? Requerimentos { get; set; }
        public ICollection<PlanoLocacao>? PlanosLocacao { get; set; }
        public ICollection<Locacao>? Locacao { get; set; }

        public ICollection<PropostaLocacao>? PropostaLocacao { get; set; } 
    }
}