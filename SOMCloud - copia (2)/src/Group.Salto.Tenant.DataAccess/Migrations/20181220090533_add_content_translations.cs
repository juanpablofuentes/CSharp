using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class add_content_translations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExternalWorkOrderStatusesTranslations",
                columns: table => new
                {
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    NameText = table.Column<string>(nullable: true),
                    DescriptionText = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false),
                    ExternalWorkOrderStatusesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalWorkOrderStatusesTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalWorkOrderStatusesTranslations_ExternalWorOrderStatuses_ExternalWorkOrderStatusesId",
                        column: x => x.ExternalWorkOrderStatusesId,
                        principalTable: "ExternalWorOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QueuesTranslations",
                columns: table => new
                {
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    NameText = table.Column<string>(nullable: true),
                    DescriptionText = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false),
                    QueuesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueuesTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueuesTranslations_Queues_QueuesId",
                        column: x => x.QueuesId,
                        principalTable: "Queues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderStatusesTranslations",
                columns: table => new
                {
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    NameText = table.Column<string>(nullable: true),
                    DescriptionText = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false),
                    WorkOrderStatusesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderStatusesTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderStatusesTranslations_WorkOrderStatuses_WorkOrderStatusesId",
                        column: x => x.WorkOrderStatusesId,
                        principalTable: "WorkOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalWorkOrderStatusesTranslations_ExternalWorkOrderStatusesId",
                table: "ExternalWorkOrderStatusesTranslations",
                column: "ExternalWorkOrderStatusesId");

            migrationBuilder.CreateIndex(
                name: "IX_QueuesTranslations_QueuesId",
                table: "QueuesTranslations",
                column: "QueuesId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderStatusesTranslations_WorkOrderStatusesId",
                table: "WorkOrderStatusesTranslations",
                column: "WorkOrderStatusesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalWorkOrderStatusesTranslations");

            migrationBuilder.DropTable(
                name: "QueuesTranslations");

            migrationBuilder.DropTable(
                name: "WorkOrderStatusesTranslations");
        }
    }
}
