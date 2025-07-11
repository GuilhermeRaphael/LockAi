using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class RepresentanteLegal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RepresentanteLegal",
                columns: new[] { "Id", "Cpf", "Email", "IdUsuario", "Nome", "Telefone" },
                values: new object[,]
                {
                    { 1, "12345678901", "mariana.alves@example.com", 1, "Mariana Alves", "11912345678" },
                    { 2, "98765432100", "carlos.henrique@example.com", 2, "Carlos Henrique", "21998765432" },
                    { 3, "45678912333", "fernanda.costa@example.com", 3, "Fernanda Costa", "31934567890" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RepresentanteLegal",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RepresentanteLegal",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RepresentanteLegal",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
