using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoLocacao_PropostaLocacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocacaoId",
                table: "Requerimentos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Locacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPropostaLocacao = table.Column<int>(type: "int", nullable: true),
                    PropostaLocacaoId = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<float>(type: "real", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DataSituacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioSituacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locacoes_PropostaLocacao_PropostaLocacaoId",
                        column: x => x.PropostaLocacaoId,
                        principalTable: "PropostaLocacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locacoes_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LocacoesParceiro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdLocacao = table.Column<int>(type: "int", nullable: false),
                    IdParceiro = table.Column<int>(type: "int", nullable: false),
                    IdentificacaoParceiro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeParceiro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DtSituacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioSituacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocacoesParceiro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocacoesParceiro_Locacoes_IdLocacao",
                        column: x => x.IdLocacao,
                        principalTable: "Locacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Requerimentos",
                keyColumn: "Id",
                keyValue: 1,
                column: "LocacaoId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Requerimentos_LocacaoId",
                table: "Requerimentos",
                column: "LocacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_IdUsuario",
                table: "Locacoes",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_PropostaLocacaoId",
                table: "Locacoes",
                column: "PropostaLocacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_LocacoesParceiro_IdLocacao",
                table: "LocacoesParceiro",
                column: "IdLocacao",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Requerimentos_Locacoes_LocacaoId",
                table: "Requerimentos",
                column: "LocacaoId",
                principalTable: "Locacoes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requerimentos_Locacoes_LocacaoId",
                table: "Requerimentos");

            migrationBuilder.DropTable(
                name: "LocacoesParceiro");

            migrationBuilder.DropTable(
                name: "Locacoes");

            migrationBuilder.DropIndex(
                name: "IX_Requerimentos_LocacaoId",
                table: "Requerimentos");

            migrationBuilder.DropColumn(
                name: "LocacaoId",
                table: "Requerimentos");
        }
    }
}
