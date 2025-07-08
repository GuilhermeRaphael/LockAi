using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


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
        public int IdTipoUsuario { get; set; }
        public string Senha { get; set; }
        public char Situacao { get; set; }
        public DateTime DtSituacao { get; set; }
        public int IdUsuarioSituacao { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        public RepresentanteLegal RepresentanteLegal { get; set; }
        public ICollection<UsuarioImagem> Imagens { get; set; }
    }
}