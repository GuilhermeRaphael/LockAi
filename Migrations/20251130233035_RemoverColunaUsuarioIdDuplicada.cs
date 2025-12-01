using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class RemoverColunaUsuarioIdDuplicada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objetos_Usuarios_UsuarioAtualizacaoId",
                table: "Objetos");

            migrationBuilder.DropForeignKey(
                name: "FK_Objetos_Usuarios_UsuarioInclusaoId",
                table: "Objetos");

            migrationBuilder.DropForeignKey(
                name: "FK_PropostaLocacao_Usuarios_IdUsuario",
                table: "PropostaLocacao");

            migrationBuilder.DropIndex(
                name: "IX_Objetos_UsuarioAtualizacaoId",
                table: "Objetos");

            migrationBuilder.DropIndex(
                name: "IX_Objetos_UsuarioInclusaoId",
                table: "Objetos");

            migrationBuilder.DropColumn(
                name: "UsuarioAtualizacaoId",
                table: "Objetos");

            migrationBuilder.DropColumn(
                name: "UsuarioInclusaoId",
                table: "Objetos");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "PropostaLocacao",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_PropostaLocacao_IdUsuario",
                table: "PropostaLocacao",
                newName: "IX_PropostaLocacao_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropostaLocacao_Usuarios_UsuarioId",
                table: "PropostaLocacao",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropostaLocacao_Usuarios_UsuarioId",
                table: "PropostaLocacao");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "PropostaLocacao",
                newName: "IdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_PropostaLocacao_UsuarioId",
                table: "PropostaLocacao",
                newName: "IX_PropostaLocacao_IdUsuario");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PropostaLocacao_Usuarios_IdUsuario",
                table: "PropostaLocacao",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
