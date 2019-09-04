using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Migrations
{
    public partial class Create_Tracker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "Trackers",
                columns: table => new
                {
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    EntityType = table.Column<string>(nullable: false),
                    PropertyName = table.Column<string>(nullable: false),
                    TimeStamp = table.Column<string>(nullable: false),
                    OldValue = table.Column<string>(nullable: false),
                    NewValue = table.Column<string>(nullable: false),
                    EntityId = table.Column<string>(nullable: false),
                    OwnerId = table.Column<string>(nullable: false),
                    TransactionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trackers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trackers");

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CultureCode", "Name", "UpdateDate" },
                values: new object[] { 1, "es", "Spanish", new DateTime(2018, 10, 30, 13, 18, 41, 250, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CultureCode", "Name", "UpdateDate" },
                values: new object[] { 2, "ca", "Catalan", new DateTime(2018, 10, 30, 13, 18, 41, 250, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CultureCode", "Name", "UpdateDate" },
                values: new object[] { 3, "en", "English", new DateTime(2018, 10, 30, 13, 18, 41, 250, DateTimeKind.Utc) });
        }
    }
}
