using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.ProjetoCursosIdioma.Migrations
{
    /// <inheritdoc />
    public partial class AnotherChangeTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AnoLetivo",
                table: "Turmas",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AnoLetivo",
                table: "Turmas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
