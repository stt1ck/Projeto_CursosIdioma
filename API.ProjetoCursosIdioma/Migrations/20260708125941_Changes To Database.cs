using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.ProjetoCursosIdioma.Migrations
{
    /// <inheritdoc />
    public partial class ChangesToDatabase : Migration
    {
        /// <inheritdoc /> Novamente essa mudança só foi feita por que não tinha dados no banco, vou buscar novas formas de fazer isso
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Turmas",
                newName: "NumeroTurma");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AnoLetivo",
                table: "Turmas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumeroTurma",
                table: "Turmas",
                newName: "Numero");

            migrationBuilder.AlterColumn<string>(
                name: "AnoLetivo",
                table: "Turmas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
