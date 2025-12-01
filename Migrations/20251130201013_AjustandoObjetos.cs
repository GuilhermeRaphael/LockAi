using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class AjustandoObjetos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PosicaoArmario",
                table: "Objetos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioAtualizacaoId",
                table: "Objetos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioInclusaoId",
                table: "Objetos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Objetos_UsuarioAtualizacaoId",
                table: "Objetos",
                column: "UsuarioAtualizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Objetos_UsuarioInclusaoId",
                table: "Objetos",
                column: "UsuarioInclusaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Objetos_Usuarios_UsuarioAtualizacaoId",
                table: "Objetos",
                column: "UsuarioAtualizacaoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Objetos_Usuarios_UsuarioInclusaoId",
                table: "Objetos",
                column: "UsuarioInclusaoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objetos_Usuarios_UsuarioAtualizacaoId",
                table: "Objetos");

            migrationBuilder.DropForeignKey(
                name: "FK_Objetos_Usuarios_UsuarioInclusaoId",
                table: "Objetos");

            migrationBuilder.DropIndex(
                name: "IX_Objetos_UsuarioAtualizacaoId",
                table: "Objetos");

            migrationBuilder.DropIndex(
                name: "IX_Objetos_UsuarioInclusaoId",
                table: "Objetos");

            migrationBuilder.DropColumn(
                name: "PosicaoArmario",
                table: "Objetos");

            migrationBuilder.DropColumn(
                name: "UsuarioAtualizacaoId",
                table: "Objetos");

            migrationBuilder.DropColumn(
                name: "UsuarioInclusaoId",
                table: "Objetos");
        }
    }
}
