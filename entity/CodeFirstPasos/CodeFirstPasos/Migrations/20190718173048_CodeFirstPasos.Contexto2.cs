using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeFirstPasos.Migrations
{
    public partial class CodeFirstPasosContexto2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apodo",
                table: "Hormigas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apodo",
                table: "Hormigas");
        }
    }
}
