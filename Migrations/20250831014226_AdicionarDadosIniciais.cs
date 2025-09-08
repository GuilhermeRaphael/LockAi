using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarDadosIniciais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Objetos",
                columns: new[] { "Id", "Descricao", "DtAtualizao", "DtInclusao", "IdTipoObjeto", "IdUsuarioAtualizacao", "IdUsuarioInclusao", "LocalidadePrimaria", "LocalidadeSecundaria", "LocalidadeTercearia", "Nome", "Situacao" },
                values: new object[] { 1, "Armário de Metal Padrão", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 1, "Bloco A", "Corredor 1", "Perto da escada", "Armário A-01", 6 });

            migrationBuilder.InsertData(
                table: "PlanoLocacao",
                columns: new[] { "Id", "DtAtualizacao", "DtFim", "DtInclusao", "DtInicio", "FimLocacao", "IdUsuarioAtualizacao", "IdUsuarioInclusao", "InicioLocacao", "Nome", "PrazoPagamento", "Situacao", "Valor" },
                values: new object[] { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "18:00", 1, 1, "08:00", "Plano Anual", 30, 1, 50f });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Objetos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PlanoLocacao",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
