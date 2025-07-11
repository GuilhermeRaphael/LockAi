using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LockAi.Models
{
    public class PropostaParceiro
    {
        public int Id { get; set; }
        public string IdentificacaoParceiro { get; set; }
        public string NomeParceiro { get; set; }
        public int IdParceiro { get; set; }
        public LocacaoParceiro LocacaoParceiro { get; set; }
    }
}