using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoTipoReqController : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdUsuarioInclusão",
                table: "TiposRequerimento",
                newName: "IdUsuarioInclusao");

            migrationBuilder.RenameColumn(
                name: "DataInclusão",
                table: "TiposRequerimento",
                newName: "DataInclusao");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioAtualizacaoId",
                table: "TiposRequerimento",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioInclusaoId",
                table: "TiposRequerimento",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TiposRequerimento",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "UsuarioAtualizacaoId", "UsuarioInclusaoId" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_TiposRequerimento_UsuarioAtualizacaoId",
                table: "TiposRequerimento",
                column: "UsuarioAtualizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposRequerimento_UsuarioInclusaoId",
                table: "TiposRequerimento",
                column: "UsuarioInclusaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TiposRequerimento_Usuarios_UsuarioAtualizacaoId",
                table: "TiposRequerimento",
                column: "UsuarioAtualizacaoId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TiposRequerimento_Usuarios_UsuarioInclusaoId",
                table: "TiposRequerimento",
                column: "UsuarioInclusaoId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TiposRequerimento_Usuarios_UsuarioAtualizacaoId",
                table: "TiposRequerimento");

            migrationBuilder.DropForeignKey(
                name: "FK_TiposRequerimento_Usuarios_UsuarioInclusaoId",
                table: "TiposRequerimento");

            migrationBuilder.DropIndex(
                name: "IX_TiposRequerimento_UsuarioAtualizacaoId",
                table: "TiposRequerimento");

            migrationBuilder.DropIndex(
                name: "IX_TiposRequerimento_UsuarioInclusaoId",
                table: "TiposRequerimento");

            migrationBuilder.DropColumn(
                name: "UsuarioAtualizacaoId",
                table: "TiposRequerimento");

            migrationBuilder.DropColumn(
                name: "UsuarioInclusaoId",
                table: "TiposRequerimento");

            migrationBuilder.RenameColumn(
                name: "IdUsuarioInclusao",
                table: "TiposRequerimento",
                newName: "IdUsuarioInclusão");

            migrationBuilder.RenameColumn(
                name: "DataInclusao",
                table: "TiposRequerimento",
                newName: "DataInclusão");
        }
    }
}
