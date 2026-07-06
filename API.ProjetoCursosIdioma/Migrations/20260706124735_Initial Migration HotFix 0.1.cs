using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.ProjetoCursosIdioma.Migrations
{
    /// <inheritdoc />
    /// 
    /// Esse HotFix só foi feito por que não tinha dados no banco, imagino que isso com certeza não é algo que se deva fazer
    public partial class InitialMigrationHotFix01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cpf",
                table: "Alunos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
