using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LockAi.Dtos
{
    public class RegistrarUsuarioDto
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public DateTime DtNascimento { get; set; }
        public string Telefone { get; set; }
        public int TipoUsuarioId { get; set; }
        public string Senha { get; set; }
    }
}