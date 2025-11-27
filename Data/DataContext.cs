using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LockAi.Models;
using LockAi.Models.Enuns;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
        public DbSet<TipoObjeto> TiposObjeto { get; set; }
        public DbSet<PlanoLocacao> PlanosLocacao { get; set; }
        public DbSet<PlanoLocacaoObjeto> PlanosLocacoesObjeto { get; set; }
        public DbSet<PropostaLocacao> PropostaLocacao { get; set; }
        public DbSet<PropostaLocacaoPagamento> PropostaLocacaoPagamento { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<LocacaoParceiro> LocacoesParceiro { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tipos de usuário
            modelBuilder.Entity<TipoUsuario>().HasData(
                new TipoUsuario { Id = 1, Nome = "Usuario" },
                new TipoUsuario { Id = 2, Nome = "Gestor" },
                new TipoUsuario { Id = 3, Nome = "Financeiro" }
            );

            // Representantes legais
            modelBuilder.Entity<RepresentanteLegal>().HasData(
                new RepresentanteLegal { Id = 1, Nome = "Mariana Alves", Cpf = "12345678901", Telefone = "11912345678", Email = "mariana.alves@example.com" },
                new RepresentanteLegal { Id = 2, Nome = "Carlos Henrique", Cpf = "98765432100", Telefone = "21998765432", Email = "carlos.henrique@example.com" },
                new RepresentanteLegal { Id = 3, Nome = "Fernanda Costa", Cpf = "45678912333", Telefone = "31934567890", Email = "fernanda.costa@example.com" }
            );

            // Usuário técnico com ID 1 para referência nas FKs
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nome = "Usuário do Sistema",
                    Cpf = "00000000000",
                    Login = "sistema",
                    Email = "sistema@lockai.com",
                    DtNascimento = new DateTime(1990, 1, 1),
                    Telefone = "0000000000",
                    TipoUsuarioId = 1,
                    Senha = "senha123",
                    Situacao = SituacaoUsuario.Ativo,
                    DtSituacao = new DateTime(2025, 09, 09),
                    IdUsuarioSituacao = 1,
                    RepresentanteLegalId = 1
                }
            );

            // Tipo de requerimento referenciando o usuário técnico
            modelBuilder.Entity<TipoRequerimento>().HasData(
                new TipoRequerimento
                {
                    Id = 1,
                    Nome = "Trancamento de Matrícula",
                    Descricao = "Solicitação para trancar matrícula do semestre",
                    Valor = 0f,
                    Situacao = SituacaoTipoRequerimentoEnum.EmAnalise,
                    DataInclusao = new DateTime(2025, 8, 26),
                    IdUsuarioInclusao = 1,
                    DataAlteracao = new DateTime(2025, 8, 26),
                    IdUsuarioAtualizacao = 1
                }
            );
/*
            // Requerimento referenciando o usuário técnico
            modelBuilder.Entity<Requerimento>().HasData(
                new Requerimento
                {
                    Id = 1,
                    Momento = new DateTime(2025, 8, 26, 10, 0, 0),
                    TipoRequerimentoId = 1,
                    IdLocacao = 101,
                    Observacao = "Solicitação enviada pelo aluno João",
                    Situacao = SituacaoRequerimentoEnum.EmAnalise,
                    DataAtualizacao = new DateTime(2025, 8, 26, 10, 0, 0),
                    UsuarioId = 1
                }
            );  */
            // Plano de locação referenciando o usuário técnico
            modelBuilder.Entity<PlanoLocacao>().HasData(
                new PlanoLocacao
                {
                    Id = 1,
                    Nome = "Plano Mensal Armário",
                    DtInicio = new DateTime(2025, 9, 8),
                    DtFim = new DateTime(2025, 10, 8, 23, 59, 59),
                    Valor = 59.90f,
                    InicioLocacao = "08:00",
                    FimLocacao = "22:00",
                    PrazoPagamento = 5,
                    Situacao = SituacaoPlanoLocacao.Ativo,
                    DtInclusao = new DateTime(2025, 9, 8),
                    IdUsuarioInclusao = 1,
                    DtAtualizacao = new DateTime(2025, 9, 8),
                    IdUsuarioAtualizacao = 1,
                    UsuarioId = 1
                }
            );


            /*
                        modelBuilder.Entity<PropostaLocacaoPagamento>().HasData(
                new PropostaLocacaoPagamento
                {
                    Id = 1,
                    Data = new DateTime(2025, 9, 9),
                    Comprovante = "comprovante1.jpg",
                    IdUsuario = 1, // ← nome deve bater com o modelo
                    DtConferencia = new DateTime(2025, 9, 10),
                    IdUsuarioConferencia = 1,
                    Situacao = SituacaoPropostaLocacaoPagamento.Aprovado,
                    IdPropostaLocacao = 1
                }
            );


                        modelBuilder.Entity<PropostaLocacao>().HasData(
                            new PropostaLocacao
                            {
                                Id = 1,
                                Data = new DateTime(2025, 9, 9),
                                IdUsuario = 1,
                                IdObjeto = 1,
                                IdPlanoLocacao = 1,
                                DtInicio = new DateTime(2025, 9, 10),
                                DtFim = new DateTime(2025, 10, 10),
                                DtValidade = new DateTime(2025, 9, 15),
                                Valor = 59.90f,
                                Situacao = SituacaoPropostaEnum.Aprovada,
                                DtSituacao = new DateTime(2025, 9, 10),
                                IdUsuarioSituacao = 1
                            }
                        );


            */
            // Relacionamentos
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.RepresentanteLegal)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RepresentanteLegalId);

            modelBuilder.Entity<UsuarioImagem>().HasKey(ui => ui.IdImagem);

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
                .HasForeignKey(u => u.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlanoLocacao>()
                .HasOne(p => p.UsuarioInclusao)
                .WithMany()
                .HasForeignKey(p => p.IdUsuarioInclusao)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlanoLocacao>()
                .HasOne(p => p.UsuarioAtualizacao)
                .WithMany()
                .HasForeignKey(p => p.IdUsuarioAtualizacao)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PlanoLocacaoObjeto>()
                .HasKey(po => new { po.IdPlanoLocacao, po.IdTipoObjeto });
            /*modelBuilder.Entity<PlanoLocacaoObjeto>()
                .HasOne(p => p.PlanoLocacao)
                .WithMany(o => o.PlanoLocacaoObjetos)
                .HasForeignKey(p => p.IdPlanoLocacao); */


            modelBuilder.Entity<PlanoLocacaoObjeto>()
                .HasOne(po => po.PlanoLocacao)
                .WithMany(p => p.PlanoLocacaoObjetos)
                .HasForeignKey(po => po.IdPlanoLocacao)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlanoLocacaoObjeto>()
                .HasOne(po => po.TipoObjeto)
                .WithMany(t => t.PlanoLocacaoObjetos)
                .HasForeignKey(po => po.IdTipoObjeto)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Objeto>()
                .HasOne(u => u.TipoObjeto)
                .WithMany(r => r.Objeto)
                .HasForeignKey(p => p.IdTipoObjeto);

            modelBuilder.Entity<Locacao>()
                .HasOne(e => e.LocacaoParceiro)
                .WithOne(e => e.Locacao)
                .HasForeignKey<LocacaoParceiro>(e => e.IdLocacao)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Locacao)
                .WithOne(e => e.Usuario)
                .HasForeignKey(e => e.IdUsuario)
                .IsRequired(false);

            modelBuilder.Entity<Locacao>()
                .HasMany(e => e.Requerimento)
                .WithOne(e => e.Locacao)
                .HasForeignKey(e => e.IdLocacao)
                .IsRequired(false);

            modelBuilder.Entity<PropostaLocacao>()
                .HasOne(u => u.PlanoLocacao)
                .WithMany(r => r.PropostaLocacao)
                .HasForeignKey(u => u.IdPlanoLocacao);
            
             modelBuilder.Entity<PropostaLocacao>()
                .Property(p => p.IdPlanoLocacao)
                .IsRequired();

            modelBuilder.Entity<PropostaLocacao>()
                .HasOne(p => p.Objeto)
                .WithMany(o => o.PropostaLocacao)
                .HasForeignKey(p => p.IdObjeto)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PropostaLocacao>()
            .HasOne(p => p.Pagamento)
            .WithOne(p => p.PropostaLocacao)
            .HasForeignKey<PropostaLocacaoPagamento>(p => p.IdPropostaLocacao)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PropostaLocacao>()
                .HasOne(u => u.Locacao)
                .WithOne(r => r.PropostaLocacao)
                .HasForeignKey<Locacao>(u => u.IdPropostaLocacao)
                .IsRequired(false);
           
            modelBuilder.Entity<PropostaLocacao>()
                .HasOne(u => u.Usuario)
                .WithMany(r => r.PropostaLocacao)
                .HasForeignKey(u => u.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);
           

        }
    }
}