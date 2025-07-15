using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LockAi.Dtos
{
    public class AlterarEmailDto
    {
        public string Email { get; set; }

        // DTO (Data Transfer Object)
        // Usado para receber ou enviar apenas os dados necessários entre o frontend e a API.
        // Isso evita expor ou alterar todo o objeto da model (RepresentanteLegal) acidentalmente.
        // Neste caso, usamos o DTO para alterar apenas o email do representante legal.
    }
}