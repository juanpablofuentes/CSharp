using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeFirstPasos.Migrations
{
    public partial class CodeFirstPasosContexto4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Vuela",
                table: "Hormigas",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vuela",
                table: "Hormigas");
        }
    }
}
