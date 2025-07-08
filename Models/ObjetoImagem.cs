using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LockAi.Models
{
    public class ObjetoImagem
    {
        public int IdImagem { get; set; }
        public int IdObjeto { get; set; }
        public Objeto objeto { get; set; }
        public string EndImagem { get; set; }
        public ICollection<ObjetoImagem> Imagens { get; set; }
    }
}