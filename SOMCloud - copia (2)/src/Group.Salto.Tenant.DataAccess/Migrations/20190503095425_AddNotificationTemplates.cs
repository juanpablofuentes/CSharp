using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class AddNotificationTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "PeopleNotifications",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NotificationTemplateTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplateTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeopleNotificationTranslation",
                columns: table => new
                {
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    NameText = table.Column<string>(nullable: true),
                    DescriptionText = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false),
                    PeopleNotificationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleNotificationTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeopleNotificationTranslation_PeopleNotifications_PeopleNotificationId",
                        column: x => x.PeopleNotificationId,
                        principalTable: "PeopleNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    PeopleNotificationTemplateTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeopleNotificationTemplate_TemplateType",
                        column: x => x.PeopleNotificationTemplateTypeId,
                        principalTable: "NotificationTemplateTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTemplateTranslation",
                columns: table => new
                {
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    NameText = table.Column<string>(nullable: true),
                    DescriptionText = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false),
                    NotificationTemplateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplateTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationTemplateTranslation_NotificationTemplates_NotificationTemplateId",
                        column: x => x.NotificationTemplateId,
                        principalTable: "NotificationTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_PeopleNotificationTemplateTypeId",
                table: "NotificationTemplates",
                column: "PeopleNotificationTemplateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplateTranslation_NotificationTemplateId",
                table: "NotificationTemplateTranslation",
                column: "NotificationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleNotificationTranslation_PeopleNotificationId",
                table: "PeopleNotificationTranslation",
                column: "PeopleNotificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationTemplateTranslation");

            migrationBuilder.DropTable(
                name: "PeopleNotificationTranslation");

            migrationBuilder.DropTable(
                name: "NotificationTemplates");

            migrationBuilder.DropTable(
                name: "NotificationTemplateTypes");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "PeopleNotifications");
        }
    }
}
