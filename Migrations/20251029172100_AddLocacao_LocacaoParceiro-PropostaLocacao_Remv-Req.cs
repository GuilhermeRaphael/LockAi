using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class AddLocacao_LocacaoParceiroPropostaLocacao_RemvReq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropostaLocacao_Objetos_ObjetoId",
                table: "PropostaLocacao");

            migrationBuilder.DropForeignKey(
                name: "FK_PropostaLocacao_PlanosLocacao_PlanoLocacaoId",
                table: "PropostaLocacao");

            migrationBuilder.DropForeignKey(
                name: "FK_PropostaLocacao_Usuarios_UsuarioId",
                table: "PropostaLocacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropostaLocacao",
                table: "PropostaLocacao");

            migrationBuilder.DropIndex(
                name: "IX_PropostaLocacao_ObjetoId",
                table: "PropostaLocacao");

            migrationBuilder.DropIndex(
                name: "IX_PropostaLocacao_PlanoLocacaoId",
                table: "PropostaLocacao");

            migrationBuilder.DeleteData(
                table: "Requerimentos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "ObjetoId",
                table: "PropostaLocacao");

            migrationBuilder.DropColumn(
                name: "PlanoLocacaoId",
                table: "PropostaLocacao");

            migrationBuilder.RenameTable(
                name: "PropostaLocacao",
                newName: "PropostasLocacao");

            migrationBuilder.RenameIndex(
                name: "IX_PropostaLocacao_UsuarioId",
                table: "PropostasLocacao",
                newName: "IX_PropostasLocacao_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropostasLocacao",
                table: "PropostasLocacao",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Locacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPropostaLocacao = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Locacoes_PropostasLocacao_IdPropostaLocacao",
                        column: x => x.IdPropostaLocacao,
                        principalTable: "PropostasLocacao",
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

            migrationBuilder.CreateIndex(
                name: "IX_Requerimentos_IdLocacao",
                table: "Requerimentos",
                column: "IdLocacao");

            migrationBuilder.CreateIndex(
                name: "IX_PropostasLocacao_IdObjeto",
                table: "PropostasLocacao",
                column: "IdObjeto");

            migrationBuilder.CreateIndex(
                name: "IX_PropostasLocacao_IdPlanoLocacao",
                table: "PropostasLocacao",
                column: "IdPlanoLocacao");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_IdPropostaLocacao",
                table: "Locacoes",
                column: "IdPropostaLocacao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_IdUsuario",
                table: "Locacoes",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_LocacoesParceiro_IdLocacao",
                table: "LocacoesParceiro",
                column: "IdLocacao",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PropostasLocacao_Objetos_IdObjeto",
                table: "PropostasLocacao",
                column: "IdObjeto",
                principalTable: "Objetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropostasLocacao_PlanosLocacao_IdPlanoLocacao",
                table: "PropostasLocacao",
                column: "IdPlanoLocacao",
                principalTable: "PlanosLocacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropostasLocacao_Usuarios_UsuarioId",
                table: "PropostasLocacao",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requerimentos_Locacoes_IdLocacao",
                table: "Requerimentos",
                column: "IdLocacao",
                principalTable: "Locacoes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropostasLocacao_Objetos_IdObjeto",
                table: "PropostasLocacao");

            migrationBuilder.DropForeignKey(
                name: "FK_PropostasLocacao_PlanosLocacao_IdPlanoLocacao",
                table: "PropostasLocacao");

            migrationBuilder.DropForeignKey(
                name: "FK_PropostasLocacao_Usuarios_UsuarioId",
                table: "PropostasLocacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Requerimentos_Locacoes_IdLocacao",
                table: "Requerimentos");

            migrationBuilder.DropTable(
                name: "LocacoesParceiro");

            migrationBuilder.DropTable(
                name: "Locacoes");

            migrationBuilder.DropIndex(
                name: "IX_Requerimentos_IdLocacao",
                table: "Requerimentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropostasLocacao",
                table: "PropostasLocacao");

            migrationBuilder.DropIndex(
                name: "IX_PropostasLocacao_IdObjeto",
                table: "PropostasLocacao");

            migrationBuilder.DropIndex(
                name: "IX_PropostasLocacao_IdPlanoLocacao",
                table: "PropostasLocacao");

            migrationBuilder.RenameTable(
                name: "PropostasLocacao",
                newName: "PropostaLocacao");

            migrationBuilder.RenameIndex(
                name: "IX_PropostasLocacao_UsuarioId",
                table: "PropostaLocacao",
                newName: "IX_PropostaLocacao_UsuarioId");

            migrationBuilder.AddColumn<int>(
                name: "ObjetoId",
                table: "PropostaLocacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlanoLocacaoId",
                table: "PropostaLocacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropostaLocacao",
                table: "PropostaLocacao",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Requerimentos",
                columns: new[] { "Id", "DataAtualizacao", "IdLocacao", "IdUsuarioAtualizacao", "Momento", "Observacao", "Situacao", "TipoRequerimentoId", "UsuarioId" },
                values: new object[] { 1, new DateTime(2025, 8, 26, 10, 0, 0, 0, DateTimeKind.Unspecified), 101, 0, new DateTime(2025, 8, 26, 10, 0, 0, 0, DateTimeKind.Unspecified), "Solicitação enviada pelo aluno João", 3, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_PropostaLocacao_ObjetoId",
                table: "PropostaLocacao",
                column: "ObjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_PropostaLocacao_PlanoLocacaoId",
                table: "PropostaLocacao",
                column: "PlanoLocacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropostaLocacao_Objetos_ObjetoId",
                table: "PropostaLocacao",
                column: "ObjetoId",
                principalTable: "Objetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropostaLocacao_PlanosLocacao_PlanoLocacaoId",
                table: "PropostaLocacao",
                column: "PlanoLocacaoId",
                principalTable: "PlanosLocacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropostaLocacao_Usuarios_UsuarioId",
                table: "PropostaLocacao",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
