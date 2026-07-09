using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.ProjetoCursosIdioma.Migrations
{
    /// <inheritdoc />
    public partial class TurmaNoDuplicateRule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_NivelTurma_NivelTurmaId",
                table: "Turmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NivelTurma",
                table: "NivelTurma");

            migrationBuilder.RenameTable(
                name: "NivelTurma",
                newName: "NivelTurmas");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroTurma",
                table: "Turmas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Turmas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NivelTurmas",
                table: "NivelTurmas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_Name_NivelTurmaId_AnoLetivo_NumeroTurma",
                table: "Turmas",
                columns: new[] { "Name", "NivelTurmaId", "AnoLetivo", "NumeroTurma" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_NivelTurmas_NivelTurmaId",
                table: "Turmas",
                column: "NivelTurmaId",
                principalTable: "NivelTurmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_NivelTurmas_NivelTurmaId",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_Name_NivelTurmaId_AnoLetivo_NumeroTurma",
                table: "Turmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NivelTurmas",
                table: "NivelTurmas");

            migrationBuilder.RenameTable(
                name: "NivelTurmas",
                newName: "NivelTurma");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroTurma",
                table: "Turmas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Turmas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NivelTurma",
                table: "NivelTurma",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_NivelTurma_NivelTurmaId",
                table: "Turmas",
                column: "NivelTurmaId",
                principalTable: "NivelTurma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
