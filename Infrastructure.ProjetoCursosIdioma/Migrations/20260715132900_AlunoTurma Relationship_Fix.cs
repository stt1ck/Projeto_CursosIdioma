using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.ProjetoCursosIdioma.Migrations
{
    /// <inheritdoc />
    public partial class AlunoTurmaRelationship_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlunoTurmas",
                columns: table => new
                {
                    AlunoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoTurmas", x => new { x.AlunoId, x.TurmaId });
                    table.ForeignKey(
                        name: "FK_AlunoTurmas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlunoTurmas_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlunoTurmas_TurmaId",
                table: "AlunoTurmas",
                column: "TurmaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoTurmas");
        }
    }
}
