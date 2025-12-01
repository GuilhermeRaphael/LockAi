using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class NovaPropostaLocacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropostaLocacao_Usuarios_UsuarioId",
                table: "PropostaLocacao");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "PropostaLocacao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "PropostaLocacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PropostaLocacao_Usuarios_UsuarioId",
                table: "PropostaLocacao",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropostaLocacao_Usuarios_UsuarioId",
                table: "PropostaLocacao");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "PropostaLocacao");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "PropostaLocacao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PropostaLocacao_Usuarios_UsuarioId",
                table: "PropostaLocacao",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
