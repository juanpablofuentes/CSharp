using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Migrations
{
    public partial class ChangesLanguagesId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "AspNetUsers",
                nullable: true,
                defaultValue: "1");

            migrationBuilder.UpdateData("AspNetUsers", "Language", "es-ES", "LanguageId", "1");
            migrationBuilder.UpdateData("AspNetUsers", "Language", "ca-ES", "LanguageId", "2");
            migrationBuilder.UpdateData("AspNetUsers", "Language", "es", "LanguageId", "3");
            migrationBuilder.Sql("update dbo.AspNetUsers set LanguageId = 1 where LanguageId is null ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "AspNetUsers");
        }
    }
}