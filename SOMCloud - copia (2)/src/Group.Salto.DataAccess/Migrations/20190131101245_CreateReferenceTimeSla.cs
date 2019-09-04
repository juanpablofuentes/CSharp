using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Migrations
{
    public partial class CreateReferenceTimeSla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReferenceTimeSla",
                columns: table => new
                {
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceTimeSla", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReferenceTimeSla");
        }
    }
}
