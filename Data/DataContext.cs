using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LockAi.Models;
using LockAi.Models.Enuns;

namespace LockAi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoUsuario> TiposUsuario { get; set; }
        public DbSet<UsuarioImagem> UsuarioImagens { get; set; }
        public DbSet<RepresentanteLegal> RepresentanteLegal { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoUsuario>().HasData
            (
                new TipoUsuario { Id = 1, Nome = "Usuario" },
                new TipoUsuario { Id = 2, Nome = "Gestor" },
                new TipoUsuario { Id = 3, Nome = "Financeiro" }
            );


            modelBuilder.Entity<Usuario>().HasData
            (
                new Usuario() { Id = 1, Nome = "Admin", Cpf = "46284605874", Login = "ADM", Email = "adm@gmail.com", DtNascimento = new DateTime(2006, 4, 7), Telefone = "11971949976", IdTipoUsuario = 2, Senha = "*123456HAS*", Situacao = SituacaoUsuario.Ativo, DtSituacao = new DateTime(2025, 7, 10), IdUsuarioSituacao = 1, RepresentanteLegalId = null }
            );

             modelBuilder.Entity<RepresentanteLegal>().HasData
            (
                new RepresentanteLegal() {Id = 1,Nome = "Mariana Alves",Cpf = "12345678901",Telefone = "11912345678",Email = "mariana.alves@example.com",IdUsuario = 1},
                new RepresentanteLegal(){Id = 2,Nome = "Carlos Henrique",Cpf = "98765432100",Telefone = "21998765432",Email = "carlos.henrique@example.com",IdUsuario = 2},
                new RepresentanteLegal(){Id = 3,Nome = "Fernanda Costa",Cpf = "45678912333",Telefone = "31934567890",Email = "fernanda.costa@example.com",IdUsuario = 3}
            );

            modelBuilder.Entity<Usuario>()
            .HasOne(u => u.RepresentanteLegal)
            .WithOne(r => r.Usuario)
            .HasForeignKey<Usuario>(u => u.RepresentanteLegalId);

            modelBuilder.Entity<UsuarioImagem>()
            .HasKey(ui => ui.IdImagem);


        }

    }
}


