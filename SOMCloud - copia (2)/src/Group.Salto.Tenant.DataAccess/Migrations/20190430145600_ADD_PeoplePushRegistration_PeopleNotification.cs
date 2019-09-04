using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class ADD_PeoplePushRegistration_PeopleNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PeopleNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    PeopleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeopleNotification_People",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeoplePushRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    PeopleId = table.Column<int>(nullable: false),
                    Platform = table.Column<int>(nullable: false),
                    Manufacturer = table.Column<string>(nullable: true),
                    DeviceModel = table.Column<string>(nullable: true),
                    Version = table.Column<string>(nullable: true),
                    Idiom = table.Column<string>(nullable: true),
                    PushToken = table.Column<string>(nullable: true),
                    DeviceId = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeoplePushRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeoplePushRegistration_People",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeopleNotifications_PeopleId",
                table: "PeopleNotifications",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_PeoplePushRegistrations_PeopleId",
                table: "PeoplePushRegistrations",
                column: "PeopleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeopleNotifications");

            migrationBuilder.DropTable(
                name: "PeoplePushRegistrations");
        }
    }
}
