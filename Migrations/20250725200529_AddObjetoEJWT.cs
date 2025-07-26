using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class AddObjetoEJWT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Objetos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalidadePrimaria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalidadeSecundaria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalidadeTercearia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    IdTipoObjeto = table.Column<int>(type: "int", nullable: false),
                    DtInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioInclusao = table.Column<int>(type: "int", nullable: false),
                    DtAtualizao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioAtualizacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objetos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Objetos");
        }
    }
}
