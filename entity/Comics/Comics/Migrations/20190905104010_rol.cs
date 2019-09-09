using Microsoft.EntityFrameworkCore.Migrations;

namespace Comics.Migrations
{
    public partial class rol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "ComicAutor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rol",
                table: "ComicAutor");
        }
    }
}
