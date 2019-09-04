using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class TasksTranslationsFlowsPublished : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Flows",
                nullable: false,
                defaultValueSql: "('true')");

            migrationBuilder.CreateTable(
                name: "TasksTranslations",
                columns: table => new
                {
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    NameText = table.Column<string>(nullable: true),
                    DescriptionText = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false),
                    TasksId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TasksTranslations_Tasks_TasksId",
                        column: x => x.TasksId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TasksTranslations_TasksId",
                table: "TasksTranslations",
                column: "TasksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TasksTranslations");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "Flows");
        }
    }
}
