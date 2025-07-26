using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LockAi.Extensions
{
    public static class ClaimTypesExtesion
    {
        public static int UsuarioId(this ClaimsPrincipal user)
        {
            try
            {
                var usuarioId = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
                return int.Parse(usuarioId);
            }
            catch
            {
                return 0;
            }
        }  //  recupera o Id do usuário logado a partir do token JWT  
           // forma de identificar o Id do usuário que está autenticado, carregar um objeto do tipo Usuário através deste id (deve ser implementado nas Controllers).

        public static string TipoUsuario(this ClaimsPrincipal user)
        {
            try
            {
                var tipoUsuario = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value ?? string.Empty;
                return tipoUsuario;
            }
            catch
            {
                return string.Empty;
            }
        } // Ele procura no token JWT um claim com tipo Role (ou seja, o perfil ou tipo do usuário). Se encontrar, retorna o valor do claim (por exemplo: "Administrador" ou "Aluno").
    }
}