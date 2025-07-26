using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LockAi.Data;
using LockAi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using LockAi.Utils;
using LockAi.Models.Enuns;
using LockAi.Dtos;
using Microsoft.AspNetCore.Authorization;

//PDF JWT 6!!!

namespace LockAi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public UsuariosController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            // PDF JWT 4 - falta inplementar em outras Classes/Controller.
        }

        private string CriarToken(Usuario usuario)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Role, usuario.TipoUsuario.Nome)
                // Claims do JWT: ID, Nome do usuário, Tipo de usuario, autenticado para identificação em requisições futuras.
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("ConfiguracaoToken:Chave").Value));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        // Identifica se usuario existe no banco.
        private async Task<bool> UsuarioExistente(string login)
        {
            if (await _context.Usuarios.AnyAsync(x => x.Login.ToLower() == login.ToLower()))
            {
                return true;
            }
            return false;

            //Pode ser desnecesario!!
            //PDF Usuario ---> PDF JWT
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioId(int id)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                .Include(u => u.RepresentanteLegal)
                .FirstOrDefaultAsync(uBusca => uBusca.Id == id);

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar usuário: {ex.Message}");
            }
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarUsuario(RegistrarUsuarioDto dto)
        {
            try
            {
                if (await UsuarioExistente(dto.Login))
                    throw new Exception("Nome de login já existente");

                Criptografia.CriarPasswordHash(dto.Senha, out byte[] hash, out byte[] salt);

                Usuario user = new Usuario
                {
                    Nome = dto.Nome,
                    Cpf = dto.Cpf,
                    Login = dto.Login,
                    Email = dto.Email,
                    DtNascimento = dto.DtNascimento,
                    Telefone = dto.Telefone,
                    TipoUsuarioId = dto.TipoUsuarioId,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    Situacao = SituacaoUsuario.Ativo,
                    DtSituacao = DateTime.Now,
                    IdUsuarioSituacao = 1,
                    Imagens = new List<UsuarioImagem>()
                };

                await _context.Usuarios.AddAsync(user);
                await _context.SaveChangesAsync();

                return Ok(user.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous] //concede uma exceção de acesso a anonimos (Teste).
        [HttpPost("Autenticar")]
        public async Task<IActionResult> AutenticarUsuario(LoginDto credenciais)
        {
            try
            {
                Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Login.ToLower().Equals(credenciais.Login.ToLower()));

                if (usuario == null)
                {
                    throw new System.Exception("Usuario não encontrado.");
                }
                else if (!Criptografia.VerificarPasswordHash(credenciais.Senha, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    throw new System.Exception("Senha incorreta.");
                }
                else
                {
                    usuario.PasswordHash = null;
                    usuario.PasswordSalt = null;
                    usuario.Token = CriarToken(usuario);
                    return Ok(usuario);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } //O método acima consultará o login no banco de dados, e caso este login não exista, retornará mensagem. Caso o login exista, este login e senha serão enviados para a API, sendo criptografados e comparados com os registros do banco de dados. Se a senha for incorreta, retornará mensagem. Caso os dados estejam corretos, será devolvido o Id deste usuário.

        [HttpPost]
        public async Task<IActionResult> AddUsuario([FromBody] Usuario novoUsuario)
        {
            try
            {
                ValidarUsuario(novoUsuario); //vai usar o metodo de validacao para validar o usuario e o representante
                _context.Usuarios.Add(novoUsuario); //adiciona no banco
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUsuarioId), new { id = novoUsuario.Id }, novoUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public void ValidarUsuario(Usuario usuario)
        {
            var atual = DateTime.Today; // Pega a data atual
            var idade = atual.Year - usuario.DtNascimento.Year; // Calcula idade pelo ano

            if (usuario.DtNascimento.Date > atual.AddYears(-idade)) // Ajusta se ainda não fez aniversário este ano
                idade--;

            if (idade < 18 && usuario.RepresentanteLegalId == null) // Se menor de 18 e sem representante
            {
                throw new Exception("Usuários menores de 18 anos devem ter representante legal.");
            }

            if (idade >= 18 && usuario.RepresentanteLegalId != null) // Se maior ou igual a 18 com representante
            {
                throw new Exception("Usuários maiores de 18 anos não devem ter representante legal.");
            }
        }
    }
}