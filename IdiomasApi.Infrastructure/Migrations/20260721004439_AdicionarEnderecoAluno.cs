using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdiomasApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarEnderecoAluno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Turmas",
                newName: "NumeroTurma");

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Alunos",
                type: "VARCHAR(150)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Alunos");

            migrationBuilder.RenameColumn(
                name: "NumeroTurma",
                table: "Turmas",
                newName: "Numero");
        }
    }
}
