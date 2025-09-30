using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockAi.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoObjetoObjetoPlanoLocacaoObjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanoLocacaoObjeto_PlanosLocacao_IdPlanoLocacao",
                table: "PlanoLocacaoObjeto");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanoLocacaoObjeto_TipoObjeto_TipoObjetoId",
                table: "PlanoLocacaoObjeto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoObjeto",
                table: "TipoObjeto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanoLocacaoObjeto",
                table: "PlanoLocacaoObjeto");

            migrationBuilder.DropIndex(
                name: "IX_PlanoLocacaoObjeto_IdPlanoLocacao",
                table: "PlanoLocacaoObjeto");

            migrationBuilder.RenameTable(
                name: "TipoObjeto",
                newName: "TiposObjeto");

            migrationBuilder.RenameTable(
                name: "PlanoLocacaoObjeto",
                newName: "PlanosLocacoesObjeto");

            migrationBuilder.RenameIndex(
                name: "IX_PlanoLocacaoObjeto_TipoObjetoId",
                table: "PlanosLocacoesObjeto",
                newName: "IX_PlanosLocacoesObjeto_TipoObjetoId");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioAtualizacaoId",
                table: "TiposObjeto",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioInclusaoId",
                table: "TiposObjeto",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipoObjetoId",
                table: "PlanosLocacoesObjeto",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PlanosLocacoesObjeto",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PlanoLocacaoId",
                table: "PlanosLocacoesObjeto",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TiposObjeto",
                table: "TiposObjeto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanosLocacoesObjeto",
                table: "PlanosLocacoesObjeto",
                columns: new[] { "IdPlanoLocacao", "IdTipoObjeto" });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "DtSituacao",
                value: new DateTime(2025, 9, 18, 16, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Objetos_IdTipoObjeto",
                table: "Objetos",
                column: "IdTipoObjeto");

            migrationBuilder.CreateIndex(
                name: "IX_TiposObjeto_UsuarioAtualizacaoId",
                table: "TiposObjeto",
                column: "UsuarioAtualizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposObjeto_UsuarioInclusaoId",
                table: "TiposObjeto",
                column: "UsuarioInclusaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosLocacoesObjeto_PlanoLocacaoId",
                table: "PlanosLocacoesObjeto",
                column: "PlanoLocacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Objetos_TiposObjeto_IdTipoObjeto",
                table: "Objetos",
                column: "IdTipoObjeto",
                principalTable: "TiposObjeto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanosLocacoesObjeto_PlanosLocacao_PlanoLocacaoId",
                table: "PlanosLocacoesObjeto",
                column: "PlanoLocacaoId",
                principalTable: "PlanosLocacao",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanosLocacoesObjeto_TiposObjeto_TipoObjetoId",
                table: "PlanosLocacoesObjeto",
                column: "TipoObjetoId",
                principalTable: "TiposObjeto",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TiposObjeto_Usuarios_UsuarioAtualizacaoId",
                table: "TiposObjeto",
                column: "UsuarioAtualizacaoId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TiposObjeto_Usuarios_UsuarioInclusaoId",
                table: "TiposObjeto",
                column: "UsuarioInclusaoId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objetos_TiposObjeto_IdTipoObjeto",
                table: "Objetos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanosLocacoesObjeto_PlanosLocacao_PlanoLocacaoId",
                table: "PlanosLocacoesObjeto");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanosLocacoesObjeto_TiposObjeto_TipoObjetoId",
                table: "PlanosLocacoesObjeto");

            migrationBuilder.DropForeignKey(
                name: "FK_TiposObjeto_Usuarios_UsuarioAtualizacaoId",
                table: "TiposObjeto");

            migrationBuilder.DropForeignKey(
                name: "FK_TiposObjeto_Usuarios_UsuarioInclusaoId",
                table: "TiposObjeto");

            migrationBuilder.DropIndex(
                name: "IX_Objetos_IdTipoObjeto",
                table: "Objetos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TiposObjeto",
                table: "TiposObjeto");

            migrationBuilder.DropIndex(
                name: "IX_TiposObjeto_UsuarioAtualizacaoId",
                table: "TiposObjeto");

            migrationBuilder.DropIndex(
                name: "IX_TiposObjeto_UsuarioInclusaoId",
                table: "TiposObjeto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanosLocacoesObjeto",
                table: "PlanosLocacoesObjeto");

            migrationBuilder.DropIndex(
                name: "IX_PlanosLocacoesObjeto_PlanoLocacaoId",
                table: "PlanosLocacoesObjeto");

            migrationBuilder.DropColumn(
                name: "UsuarioAtualizacaoId",
                table: "TiposObjeto");

            migrationBuilder.DropColumn(
                name: "UsuarioInclusaoId",
                table: "TiposObjeto");

            migrationBuilder.DropColumn(
                name: "PlanoLocacaoId",
                table: "PlanosLocacoesObjeto");

            migrationBuilder.RenameTable(
                name: "TiposObjeto",
                newName: "TipoObjeto");

            migrationBuilder.RenameTable(
                name: "PlanosLocacoesObjeto",
                newName: "PlanoLocacaoObjeto");

            migrationBuilder.RenameIndex(
                name: "IX_PlanosLocacoesObjeto_TipoObjetoId",
                table: "PlanoLocacaoObjeto",
                newName: "IX_PlanoLocacaoObjeto_TipoObjetoId");

            migrationBuilder.AlterColumn<int>(
                name: "TipoObjetoId",
                table: "PlanoLocacaoObjeto",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PlanoLocacaoObjeto",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoObjeto",
                table: "TipoObjeto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanoLocacaoObjeto",
                table: "PlanoLocacaoObjeto",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "DtSituacao",
                value: new DateTime(2025, 9, 14, 11, 12, 52, 304, DateTimeKind.Local).AddTicks(9662));

            migrationBuilder.CreateIndex(
                name: "IX_PlanoLocacaoObjeto_IdPlanoLocacao",
                table: "PlanoLocacaoObjeto",
                column: "IdPlanoLocacao");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanoLocacaoObjeto_PlanosLocacao_IdPlanoLocacao",
                table: "PlanoLocacaoObjeto",
                column: "IdPlanoLocacao",
                principalTable: "PlanosLocacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanoLocacaoObjeto_TipoObjeto_TipoObjetoId",
                table: "PlanoLocacaoObjeto",
                column: "TipoObjetoId",
                principalTable: "TipoObjeto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
