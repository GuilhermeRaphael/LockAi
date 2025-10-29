using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoLocacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_PropostasLocacao_IdPropostaLocacao",
                table: "Locacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_PropostasLocacao_Usuarios_UsuarioId",
                table: "PropostasLocacao");

            migrationBuilder.DropIndex(
                name: "IX_Locacoes_IdPropostaLocacao",
                table: "Locacoes");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "PropostasLocacao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdPropostaLocacao",
                table: "Locacoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_IdPropostaLocacao",
                table: "Locacoes",
                column: "IdPropostaLocacao",
                unique: true,
                filter: "[IdPropostaLocacao] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_PropostasLocacao_IdPropostaLocacao",
                table: "Locacoes",
                column: "IdPropostaLocacao",
                principalTable: "PropostasLocacao",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropostasLocacao_Usuarios_UsuarioId",
                table: "PropostasLocacao",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_PropostasLocacao_IdPropostaLocacao",
                table: "Locacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_PropostasLocacao_Usuarios_UsuarioId",
                table: "PropostasLocacao");

            migrationBuilder.DropIndex(
                name: "IX_Locacoes_IdPropostaLocacao",
                table: "Locacoes");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "PropostasLocacao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                name: "IX_Locacoes_IdPropostaLocacao",
                table: "Locacoes",
                column: "IdPropostaLocacao",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_PropostasLocacao_IdPropostaLocacao",
                table: "Locacoes",
                column: "IdPropostaLocacao",
                principalTable: "PropostasLocacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropostasLocacao_Usuarios_UsuarioId",
                table: "PropostasLocacao",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
