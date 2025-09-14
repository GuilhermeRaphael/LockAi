using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class CorrigidoEstruturaData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Requerimentos",
                keyColumn: "Id",
                keyValue: 1,
                column: "UsuarioId",
                value: 1);

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Cpf", "DtNascimento", "DtSituacao", "Email", "IdUsuarioSituacao", "Login", "Nome", "RepresentanteLegalId", "Senha", "Situacao", "Telefone", "TipoUsuarioId" },
                values: new object[] { 1, "00000000000", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 14, 11, 12, 52, 304, DateTimeKind.Local).AddTicks(9662), "sistema@lockai.com", 1, "sistema", "Usuário do Sistema", 1, "senha123", 1, "0000000000", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Requerimentos",
                keyColumn: "Id",
                keyValue: 1,
                column: "UsuarioId",
                value: 3);
        }
    }
}
