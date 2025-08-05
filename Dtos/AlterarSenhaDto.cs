using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LockAi.Dtos
{
    public class AlterarSenhaDto
    {
        public int UsuarioId { get; set; }
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
    }
}