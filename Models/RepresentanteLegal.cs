using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LockAi.Models
{
    public class RepresentanteLegal
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}