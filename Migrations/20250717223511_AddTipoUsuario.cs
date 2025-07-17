using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_TiposUsuario_TipoUsuarioId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "IdTipoUsuario",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "TipoUsuarioId",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "TipoUsuarioId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3,
                column: "TipoUsuarioId",
                value: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_TiposUsuario_TipoUsuarioId",
                table: "Usuarios",
                column: "TipoUsuarioId",
                principalTable: "TiposUsuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_TiposUsuario_TipoUsuarioId",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "TipoUsuarioId",
                table: "Usuarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdTipoUsuario",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IdTipoUsuario", "TipoUsuarioId" },
                values: new object[] { 2, null });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IdTipoUsuario", "TipoUsuarioId" },
                values: new object[] { 1, null });

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_TiposUsuario_TipoUsuarioId",
                table: "Usuarios",
                column: "TipoUsuarioId",
                principalTable: "TiposUsuario",
                principalColumn: "Id");
        }
    }
}
