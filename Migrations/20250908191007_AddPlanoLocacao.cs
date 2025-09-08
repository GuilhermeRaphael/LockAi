using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanoLocacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "TipoObjeto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DtInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioInclusao = table.Column<int>(type: "int", nullable: false),
                    DtAtualizao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioAtualizacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoObjeto", x => x.Id);
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
                    ObjetoId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_PropostaLocacao_Objetos_ObjetoId",
                        column: x => x.ObjetoId,
                        principalTable: "Objetos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "PlanoLocacaoObjeto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPlanoLocacao = table.Column<int>(type: "int", nullable: false),
                    IdTipoObjeto = table.Column<int>(type: "int", nullable: false),
                    TipoObjetoId = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DtInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioInclusao = table.Column<int>(type: "int", nullable: false),
                    DtAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioAtualizacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoLocacaoObjeto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanoLocacaoObjeto_PlanosLocacao_IdPlanoLocacao",
                        column: x => x.IdPlanoLocacao,
                        principalTable: "PlanosLocacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanoLocacaoObjeto_TipoObjeto_TipoObjetoId",
                        column: x => x.TipoObjetoId,
                        principalTable: "TipoObjeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PlanosLocacao",
                columns: new[] { "Id", "DtAtualizacao", "DtFim", "DtInclusao", "DtInicio", "FimLocacao", "IdUsuarioAtualizacao", "IdUsuarioInclusao", "InicioLocacao", "Nome", "PrazoPagamento", "Situacao", "UsuarioId", "Valor" },
                values: new object[] { 1, new DateTime(2025, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 8, 23, 59, 59, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "22:00", 1, 1, "08:00", "Plano Mensal Armário", 5, 1, 1, 59.9f });

            migrationBuilder.CreateIndex(
                name: "IX_PlanoLocacaoObjeto_IdPlanoLocacao",
                table: "PlanoLocacaoObjeto",
                column: "IdPlanoLocacao");

            migrationBuilder.CreateIndex(
                name: "IX_PlanoLocacaoObjeto_TipoObjetoId",
                table: "PlanoLocacaoObjeto",
                column: "TipoObjetoId");

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
                name: "IX_PropostaLocacao_ObjetoId",
                table: "PropostaLocacao",
                column: "ObjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_PropostaLocacao_PlanoLocacaoId",
                table: "PropostaLocacao",
                column: "PlanoLocacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PropostaLocacao_UsuarioId",
                table: "PropostaLocacao",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanoLocacaoObjeto");

            migrationBuilder.DropTable(
                name: "PropostaLocacao");

            migrationBuilder.DropTable(
                name: "TipoObjeto");

            migrationBuilder.DropTable(
                name: "PlanosLocacao");
        }
    }
}
