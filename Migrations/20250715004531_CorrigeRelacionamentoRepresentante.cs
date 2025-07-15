using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class CorrigeRelacionamentoRepresentante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_RepresentanteLegalId",
                table: "Usuarios");

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "RepresentanteLegal");

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Cpf", "DtNascimento", "DtSituacao", "Email", "IdTipoUsuario", "IdUsuarioSituacao", "Login", "Nome", "RepresentanteLegalId", "Senha", "Situacao", "Telefone", "TipoUsuarioId" },
                values: new object[] { 3, "12345678900", new DateTime(2010, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "joao.silva@example.com", 1, 1, "joaos", "João Silva", 1, "*senha123*", 1, "11987654321", null });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RepresentanteLegalId",
                table: "Usuarios",
                column: "RepresentanteLegalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_RepresentanteLegalId",
                table: "Usuarios");

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "RepresentanteLegal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "RepresentanteLegal",
                keyColumn: "Id",
                keyValue: 1,
                column: "IdUsuario",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RepresentanteLegal",
                keyColumn: "Id",
                keyValue: 2,
                column: "IdUsuario",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RepresentanteLegal",
                keyColumn: "Id",
                keyValue: 3,
                column: "IdUsuario",
                value: 3);

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Cpf", "DtNascimento", "DtSituacao", "Email", "IdTipoUsuario", "IdUsuarioSituacao", "Login", "Nome", "RepresentanteLegalId", "Senha", "Situacao", "Telefone", "TipoUsuarioId" },
                values: new object[] { 4, "12345678900", new DateTime(2010, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "joao.silva@example.com", 1, 1, "joaos", "João Silva", 1, "*senha123*", 1, "11987654321", null });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RepresentanteLegalId",
                table: "Usuarios",
                column: "RepresentanteLegalId",
                unique: true,
                filter: "[RepresentanteLegalId] IS NOT NULL");
        }
    }
}
