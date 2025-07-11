using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioImagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioImagem_Usuarios_UsuarioId",
                table: "UsuarioImagem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioImagem",
                table: "UsuarioImagem");

            migrationBuilder.RenameTable(
                name: "UsuarioImagem",
                newName: "UsuarioImagens");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioImagem_UsuarioId",
                table: "UsuarioImagens",
                newName: "IX_UsuarioImagens_UsuarioId");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "UsuarioImagens",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioImagens",
                table: "UsuarioImagens",
                column: "IdImagem");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioImagens_Usuarios_UsuarioId",
                table: "UsuarioImagens",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioImagens_Usuarios_UsuarioId",
                table: "UsuarioImagens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioImagens",
                table: "UsuarioImagens");

            migrationBuilder.RenameTable(
                name: "UsuarioImagens",
                newName: "UsuarioImagem");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioImagens_UsuarioId",
                table: "UsuarioImagem",
                newName: "IX_UsuarioImagem_UsuarioId");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "UsuarioImagem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioImagem",
                table: "UsuarioImagem",
                column: "IdImagem");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioImagem_Usuarios_UsuarioId",
                table: "UsuarioImagem",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
