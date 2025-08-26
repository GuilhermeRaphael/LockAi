using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class AddRequerimentoAddTipoRequerimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdTipoRequerimento",
                table: "Requerimentos",
                newName: "TipoRequerimentoId");

            migrationBuilder.CreateTable(
                name: "TiposRequerimento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<float>(type: "real", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DataInclusão = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioInclusão = table.Column<int>(type: "int", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioAtualizacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposRequerimento", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TiposRequerimento",
                columns: new[] { "Id", "DataAlteracao", "DataInclusão", "Descricao", "IdUsuarioAtualizacao", "IdUsuarioInclusão", "Nome", "Situacao", "Valor" },
                values: new object[] { 1, new DateTime(2025, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Solicitação para trancar matrícula do semestre", 1, 1, "Trancamento de Matrícula", 0, 0f });

            migrationBuilder.InsertData(
                table: "Requerimentos",
                columns: new[] { "Id", "DataAtualizacao", "IdLocacao", "IdUsuarioAtualizacao", "Momento", "Observacao", "Situacao", "TipoRequerimentoId", "UsuarioId" },
                values: new object[] { 1, new DateTime(2025, 8, 26, 10, 0, 0, 0, DateTimeKind.Unspecified), 101, 0, new DateTime(2025, 8, 26, 10, 0, 0, 0, DateTimeKind.Unspecified), "Solicitação enviada pelo aluno João", 3, 1, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Requerimentos_TipoRequerimentoId",
                table: "Requerimentos",
                column: "TipoRequerimentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requerimentos_TiposRequerimento_TipoRequerimentoId",
                table: "Requerimentos",
                column: "TipoRequerimentoId",
                principalTable: "TiposRequerimento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requerimentos_TiposRequerimento_TipoRequerimentoId",
                table: "Requerimentos");

            migrationBuilder.DropTable(
                name: "TiposRequerimento");

            migrationBuilder.DropIndex(
                name: "IX_Requerimentos_TipoRequerimentoId",
                table: "Requerimentos");

            migrationBuilder.DeleteData(
                table: "Requerimentos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "TipoRequerimentoId",
                table: "Requerimentos",
                newName: "IdTipoRequerimento");
        }
    }
}
