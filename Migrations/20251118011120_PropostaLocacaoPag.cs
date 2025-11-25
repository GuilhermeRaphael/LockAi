using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class PropostaLocacaoPag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RepresentanteLegal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepresentanteLegal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DtNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoUsuarioId = table.Column<int>(type: "int", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DtSituacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioSituacao = table.Column<int>(type: "int", nullable: false),
                    RepresentanteLegalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_RepresentanteLegal_RepresentanteLegalId",
                        column: x => x.RepresentanteLegalId,
                        principalTable: "RepresentanteLegal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuarios_TiposUsuario_TipoUsuarioId",
                        column: x => x.TipoUsuarioId,
                        principalTable: "TiposUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanosLocacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DtInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<float>(type: "real", nullable: false),
                    InicioLocacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FimLocacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrazoPagamento = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DtInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioInclusao = table.Column<int>(type: "int", nullable: false),
                    DtAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioAtualizacao = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosLocacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanosLocacao_Usuarios_IdUsuarioAtualizacao",
                        column: x => x.IdUsuarioAtualizacao,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanosLocacao_Usuarios_IdUsuarioInclusao",
                        column: x => x.IdUsuarioInclusao,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanosLocacao_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TiposObjeto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DtInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioInclusao = table.Column<int>(type: "int", nullable: false),
                    UsuarioInclusaoId = table.Column<int>(type: "int", nullable: true),
                    DtAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioAtualizacao = table.Column<int>(type: "int", nullable: false),
                    UsuarioAtualizacaoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposObjeto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiposObjeto_Usuarios_UsuarioAtualizacaoId",
                        column: x => x.UsuarioAtualizacaoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TiposObjeto_Usuarios_UsuarioInclusaoId",
                        column: x => x.UsuarioInclusaoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TiposRequerimento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<float>(type: "real", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioInclusao = table.Column<int>(type: "int", nullable: false),
                    UsuarioInclusaoId = table.Column<int>(type: "int", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioAtualizacao = table.Column<int>(type: "int", nullable: false),
                    UsuarioAtualizacaoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposRequerimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiposRequerimento_Usuarios_UsuarioAtualizacaoId",
                        column: x => x.UsuarioAtualizacaoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TiposRequerimento_Usuarios_UsuarioInclusaoId",
                        column: x => x.UsuarioInclusaoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioImagens",
                columns: table => new
                {
                    IdImagem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    EndImagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioImagens", x => x.IdImagem);
                    table.ForeignKey(
                        name: "FK_UsuarioImagens_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Objetos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalidadePrimaria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalidadeSecundaria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalidadeTercearia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    IdTipoObjeto = table.Column<int>(type: "int", nullable: false),
                    DtInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioInclusao = table.Column<int>(type: "int", nullable: false),
                    DtAtualizao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioAtualizacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objetos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objetos_TiposObjeto_IdTipoObjeto",
                        column: x => x.IdTipoObjeto,
                        principalTable: "TiposObjeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanosLocacoesObjeto",
                columns: table => new
                {
                    IdPlanoLocacao = table.Column<int>(type: "int", nullable: false),
                    IdTipoObjeto = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DtInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioInclusao = table.Column<int>(type: "int", nullable: false),
                    DtAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioAtualizacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosLocacoesObjeto", x => new { x.IdPlanoLocacao, x.IdTipoObjeto });
                    table.ForeignKey(
                        name: "FK_PlanosLocacoesObjeto_PlanosLocacao_IdPlanoLocacao",
                        column: x => x.IdPlanoLocacao,
                        principalTable: "PlanosLocacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanosLocacoesObjeto_TiposObjeto_IdTipoObjeto",
                        column: x => x.IdTipoObjeto,
                        principalTable: "TiposObjeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Requerimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Momento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdLocacao = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioAtualizacao = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    TipoRequerimentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requerimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requerimentos_TiposRequerimento_TipoRequerimentoId",
                        column: x => x.TipoRequerimentoId,
                        principalTable: "TiposRequerimento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requerimentos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropostaLocacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    IdObjeto = table.Column<int>(type: "int", nullable: false),
                    IdPlanoLocacao = table.Column<int>(type: "int", nullable: false),
                    PlanoLocacaoId = table.Column<int>(type: "int", nullable: false),
                    DtInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtValidade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<float>(type: "real", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DtSituacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioSituacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropostaLocacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropostaLocacao_Objetos_IdObjeto",
                        column: x => x.IdObjeto,
                        principalTable: "Objetos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropostaLocacao_PlanosLocacao_PlanoLocacaoId",
                        column: x => x.PlanoLocacaoId,
                        principalTable: "PlanosLocacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropostaLocacao_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropostaLocacaoPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comprovante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    DtConferencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioConferencia = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    IdPropostaLocacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropostaLocacaoPagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropostaLocacaoPagamento_PropostaLocacao_IdPropostaLocacao",
                        column: x => x.IdPropostaLocacao,
                        principalTable: "PropostaLocacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropostaLocacaoPagamento_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RepresentanteLegal",
                columns: new[] { "Id", "Cpf", "Email", "Nome", "Telefone" },
                values: new object[,]
                {
                    { 1, "12345678901", "mariana.alves@example.com", "Mariana Alves", "11912345678" },
                    { 2, "98765432100", "carlos.henrique@example.com", "Carlos Henrique", "21998765432" },
                    { 3, "45678912333", "fernanda.costa@example.com", "Fernanda Costa", "31934567890" }
                });

            migrationBuilder.InsertData(
                table: "TiposRequerimento",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Descricao", "IdUsuarioAtualizacao", "IdUsuarioInclusao", "Nome", "Situacao", "UsuarioAtualizacaoId", "UsuarioInclusaoId", "Valor" },
                values: new object[] { 1, new DateTime(2025, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Solicitação para trancar matrícula do semestre", 1, 1, "Trancamento de Matrícula", 0, null, null, 0f });

            migrationBuilder.InsertData(
                table: "TiposUsuario",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Usuario" },
                    { 2, "Gestor" },
                    { 3, "Financeiro" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Cpf", "DtNascimento", "DtSituacao", "Email", "IdUsuarioSituacao", "Login", "Nome", "RepresentanteLegalId", "Senha", "Situacao", "Telefone", "TipoUsuarioId" },
                values: new object[] { 1, "00000000000", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "sistema@lockai.com", 1, "sistema", "Usuário do Sistema", 1, "senha123", 1, "0000000000", 1 });

            migrationBuilder.InsertData(
                table: "PlanosLocacao",
                columns: new[] { "Id", "DtAtualizacao", "DtFim", "DtInclusao", "DtInicio", "FimLocacao", "IdUsuarioAtualizacao", "IdUsuarioInclusao", "InicioLocacao", "Nome", "PrazoPagamento", "Situacao", "UsuarioId", "Valor" },
                values: new object[] { 1, new DateTime(2025, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 8, 23, 59, 59, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "22:00", 1, 1, "08:00", "Plano Mensal Armário", 5, 1, 1, 59.9f });

            migrationBuilder.InsertData(
                table: "Requerimentos",
                columns: new[] { "Id", "DataAtualizacao", "IdLocacao", "IdUsuarioAtualizacao", "Momento", "Observacao", "Situacao", "TipoRequerimentoId", "UsuarioId" },
                values: new object[] { 1, new DateTime(2025, 8, 26, 10, 0, 0, 0, DateTimeKind.Unspecified), 101, 0, new DateTime(2025, 8, 26, 10, 0, 0, 0, DateTimeKind.Unspecified), "Solicitação enviada pelo aluno João", 3, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Objetos_IdTipoObjeto",
                table: "Objetos",
                column: "IdTipoObjeto");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosLocacao_IdUsuarioAtualizacao",
                table: "PlanosLocacao",
                column: "IdUsuarioAtualizacao");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosLocacao_IdUsuarioInclusao",
                table: "PlanosLocacao",
                column: "IdUsuarioInclusao");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosLocacao_UsuarioId",
                table: "PlanosLocacao",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosLocacoesObjeto_IdTipoObjeto",
                table: "PlanosLocacoesObjeto",
                column: "IdTipoObjeto");

            migrationBuilder.CreateIndex(
                name: "IX_PropostaLocacao_IdObjeto",
                table: "PropostaLocacao",
                column: "IdObjeto");

            migrationBuilder.CreateIndex(
                name: "IX_PropostaLocacao_PlanoLocacaoId",
                table: "PropostaLocacao",
                column: "PlanoLocacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PropostaLocacao_UsuarioId",
                table: "PropostaLocacao",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PropostaLocacaoPagamento_IdPropostaLocacao",
                table: "PropostaLocacaoPagamento",
                column: "IdPropostaLocacao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropostaLocacaoPagamento_UsuarioId",
                table: "PropostaLocacaoPagamento",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Requerimentos_TipoRequerimentoId",
                table: "Requerimentos",
                column: "TipoRequerimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Requerimentos_UsuarioId",
                table: "Requerimentos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposObjeto_UsuarioAtualizacaoId",
                table: "TiposObjeto",
                column: "UsuarioAtualizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposObjeto_UsuarioInclusaoId",
                table: "TiposObjeto",
                column: "UsuarioInclusaoId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposRequerimento_UsuarioAtualizacaoId",
                table: "TiposRequerimento",
                column: "UsuarioAtualizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposRequerimento_UsuarioInclusaoId",
                table: "TiposRequerimento",
                column: "UsuarioInclusaoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioImagens_UsuarioId",
                table: "UsuarioImagens",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RepresentanteLegalId",
                table: "Usuarios",
                column: "RepresentanteLegalId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TipoUsuarioId",
                table: "Usuarios",
                column: "TipoUsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanosLocacoesObjeto");

            migrationBuilder.DropTable(
                name: "PropostaLocacaoPagamento");

            migrationBuilder.DropTable(
                name: "Requerimentos");

            migrationBuilder.DropTable(
                name: "UsuarioImagens");

            migrationBuilder.DropTable(
                name: "PropostaLocacao");

            migrationBuilder.DropTable(
                name: "TiposRequerimento");

            migrationBuilder.DropTable(
                name: "Objetos");

            migrationBuilder.DropTable(
                name: "PlanosLocacao");

            migrationBuilder.DropTable(
                name: "TiposObjeto");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "RepresentanteLegal");

            migrationBuilder.DropTable(
                name: "TiposUsuario");
        }
    }
}
