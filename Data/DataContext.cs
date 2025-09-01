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
        public DbSet<Requerimento> Requerimentos { get; set; }
        public DbSet<TipoRequerimento> TiposRequerimento { get; set; }
        public DbSet<Objeto> Objetos { get; set; }
        public DbSet<PlanoLocacao> PlanosLocacao { get; set; }

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
                new Usuario() { Id = 1, Nome = "Admin", Cpf = "46284605874", Login = "ADM", Email = "adm@gmail.com", DtNascimento = new DateTime(2006, 4, 7), Telefone = "11971949976", TipoUsuarioId = 2, Senha = "*123456HAS*", Situacao = SituacaoUsuario.Ativo, DtSituacao = new DateTime(2025, 7, 10), IdUsuarioSituacao = 1, RepresentanteLegalId = null },
                new Usuario() { Id = 3, Nome = "João Silva", Cpf = "12345678900", Login = "joaos", Email = "joao.silva@example.com", DtNascimento = new DateTime(2010, 5, 15), Telefone = "11987654321", TipoUsuarioId = 1, Senha = "*senha123*", Situacao = SituacaoUsuario.Ativo, DtSituacao = new DateTime(2025, 7, 14), IdUsuarioSituacao = 1, RepresentanteLegalId = 1 }

            );

            modelBuilder.Entity<RepresentanteLegal>().HasData
           (
               new RepresentanteLegal() { Id = 1, Nome = "Mariana Alves", Cpf = "12345678901", Telefone = "11912345678", Email = "mariana.alves@example.com" },
               new RepresentanteLegal() { Id = 2, Nome = "Carlos Henrique", Cpf = "98765432100", Telefone = "21998765432", Email = "carlos.henrique@example.com" },
               new RepresentanteLegal() { Id = 3, Nome = "Fernanda Costa", Cpf = "45678912333", Telefone = "31934567890", Email = "fernanda.costa@example.com" }
           );

            modelBuilder.Entity<TipoRequerimento>().HasData
            (
                new TipoRequerimento() { Id = 1, Nome = "Trancamento de Matrícula", Descricao = "Solicitação para trancar matrícula do semestre", Valor = 0f, Situacao = SituacaoTipoRequerimentoEnum.EmAnalise, DataInclusao = new DateTime(2025, 8, 26), IdUsuarioInclusao = 1, DataAlteracao = new DateTime(2025, 8, 26), IdUsuarioAtualizacao = 1 }
            );


            modelBuilder.Entity<Requerimento>().HasData
            (
                new Requerimento { Id = 1, Momento = new DateTime(2025, 8, 26, 10, 0, 0), TipoRequerimentoId = 1, IdLocacao = 101, Observacao = "Solicitação enviada pelo aluno João", Situacao = SituacaoRequerimentoEnum.EmAnalise, DataAtualizacao = new DateTime(2025, 8, 26, 10, 0, 0), UsuarioId = 3 }
            );

            modelBuilder.Entity<Usuario>()
            .HasOne(u => u.RepresentanteLegal)
            .WithMany(r => r.Usuarios)
            .HasForeignKey(u => u.RepresentanteLegalId);

            modelBuilder.Entity<UsuarioImagem>()
            .HasKey(ui => ui.IdImagem);

            modelBuilder.Entity<Usuario>()
            .HasOne(u => u.TipoUsuario)
            .WithMany(t => t.Usuarios)
            .HasForeignKey(u => u.TipoUsuarioId);

            modelBuilder.Entity<Requerimento>()
            .HasOne(r => r.Usuario)
            .WithMany(u => u.Requerimentos)
            .HasForeignKey(r => r.UsuarioId);

            modelBuilder.Entity<Requerimento>()
            .HasOne(p => p.TipoRequerimento)
            .WithMany(u => u.Requerimentos) 
            .HasForeignKey(p => p.TipoRequerimentoId);

            modelBuilder.Entity<PlanoLocacao>()
            .HasOne(u => u.Usuario)
            .WithMany(p => p.PlanosLocacao)
            .HasForeignKey(u => u.UsuarioId);

            modelBuilder.Entity<PlanoLocacaoObjeto>()
            .HasOne(p => p.PlanoLocacao)
            .WithMany(o => o.PlanoLocacaoObjetos)
            .HasForeignKey(p => p.IdPlanoLocacao);
        }
    }
}


