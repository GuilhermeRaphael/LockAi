using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class AddeUpdteRelacionamentos_Endpoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_PropostaLocacao_PropostaLocacaoId",
                table: "Locacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_PropostaLocacao_PlanosLocacao_PlanoLocacaoId",
                table: "PropostaLocacao");

            migrationBuilder.DropForeignKey(
                name: "FK_PropostaLocacao_Usuarios_UsuarioId",
                table: "PropostaLocacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Requerimentos_Locacoes_LocacaoId",
                table: "Requerimentos");

            migrationBuilder.DropIndex(
                name: "IX_Requerimentos_LocacaoId",
                table: "Requerimentos");

            migrationBuilder.DropIndex(
                name: "IX_PropostaLocacao_PlanoLocacaoId",
                table: "PropostaLocacao");

            migrationBuilder.DropIndex(
                name: "IX_PropostaLocacao_UsuarioId",
                table: "PropostaLocacao");

            migrationBuilder.DropIndex(
                name: "IX_Locacoes_PropostaLocacaoId",
                table: "Locacoes");

            migrationBuilder.DeleteData(
                table: "Requerimentos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "LocacaoId",
                table: "Requerimentos");

            migrationBuilder.DropColumn(
                name: "PlanoLocacaoId",
                table: "PropostaLocacao");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "PropostaLocacao");

            migrationBuilder.DropColumn(
                name: "PropostaLocacaoId",
                table: "Locacoes");

            migrationBuilder.AlterColumn<int>(
                name: "IdPropostaLocacao",
                table: "Locacoes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requerimentos_IdLocacao",
                table: "Requerimentos",
                column: "IdLocacao");

            migrationBuilder.CreateIndex(
                name: "IX_PropostaLocacao_IdPlanoLocacao",
                table: "PropostaLocacao",
                column: "IdPlanoLocacao");

            migrationBuilder.CreateIndex(
                name: "IX_PropostaLocacao_IdUsuario",
                table: "PropostaLocacao",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_IdPropostaLocacao",
                table: "Locacoes",
                column: "IdPropostaLocacao",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_PropostaLocacao_IdPropostaLocacao",
                table: "Locacoes",
                column: "IdPropostaLocacao",
                principalTable: "PropostaLocacao",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropostaLocacao_PlanosLocacao_IdPlanoLocacao",
                table: "PropostaLocacao",
                column: "IdPlanoLocacao",
                principalTable: "PlanosLocacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropostaLocacao_Usuarios_IdUsuario",
                table: "PropostaLocacao",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Locacoes_PropostaLocacao_IdPropostaLocacao",
                table: "Locacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_PropostaLocacao_PlanosLocacao_IdPlanoLocacao",
                table: "PropostaLocacao");

            migrationBuilder.DropForeignKey(
                name: "FK_PropostaLocacao_Usuarios_IdUsuario",
                table: "PropostaLocacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Requerimentos_Locacoes_IdLocacao",
                table: "Requerimentos");

            migrationBuilder.DropIndex(
                name: "IX_Requerimentos_IdLocacao",
                table: "Requerimentos");

            migrationBuilder.DropIndex(
                name: "IX_PropostaLocacao_IdPlanoLocacao",
                table: "PropostaLocacao");

            migrationBuilder.DropIndex(
                name: "IX_PropostaLocacao_IdUsuario",
                table: "PropostaLocacao");

            migrationBuilder.DropIndex(
                name: "IX_Locacoes_IdPropostaLocacao",
                table: "Locacoes");

            migrationBuilder.AddColumn<int>(
                name: "LocacaoId",
                table: "Requerimentos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanoLocacaoId",
                table: "PropostaLocacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "PropostaLocacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "IdPropostaLocacao",
                table: "Locacoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PropostaLocacaoId",
                table: "Locacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Requerimentos",
                columns: new[] { "Id", "DataAtualizacao", "IdLocacao", "IdUsuarioAtualizacao", "LocacaoId", "Momento", "Observacao", "Situacao", "TipoRequerimentoId", "UsuarioId" },
                values: new object[] { 1, new DateTime(2025, 8, 26, 10, 0, 0, 0, DateTimeKind.Unspecified), 101, 0, null, new DateTime(2025, 8, 26, 10, 0, 0, 0, DateTimeKind.Unspecified), "Solicitação enviada pelo aluno João", 3, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Requerimentos_LocacaoId",
                table: "Requerimentos",
                column: "LocacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PropostaLocacao_PlanoLocacaoId",
                table: "PropostaLocacao",
                column: "PlanoLocacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PropostaLocacao_UsuarioId",
                table: "PropostaLocacao",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_PropostaLocacaoId",
                table: "Locacoes",
                column: "PropostaLocacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_PropostaLocacao_PropostaLocacaoId",
                table: "Locacoes",
                column: "PropostaLocacaoId",
                principalTable: "PropostaLocacao",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Requerimentos_Locacoes_LocacaoId",
                table: "Requerimentos",
                column: "LocacaoId",
                principalTable: "Locacoes",
                principalColumn: "Id");
        }
    }
}
