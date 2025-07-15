using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class AlterarRepresentanteLegal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Cpf", "DtNascimento", "DtSituacao", "Email", "IdTipoUsuario", "IdUsuarioSituacao", "Login", "Nome", "RepresentanteLegalId", "Senha", "Situacao", "Telefone", "TipoUsuarioId" },
                values: new object[] { 4, "12345678900", new DateTime(2010, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "joao.silva@example.com", 1, 1, "joaos", "João Silva", 1, "*senha123*", 1, "11987654321", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
