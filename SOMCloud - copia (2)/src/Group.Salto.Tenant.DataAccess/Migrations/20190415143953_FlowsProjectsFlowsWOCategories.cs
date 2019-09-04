using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class FlowsProjectsFlowsWOCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlowsProjects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    FlowId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowsProjects", x => new { x.FlowId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_FlowsProjects_Flows_FlowId",
                        column: x => x.FlowId,
                        principalTable: "Flows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowsProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowsWOCategories",
                columns: table => new
                {
                    WOCategoryId = table.Column<int>(nullable: false),
                    FlowId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowsWOCategories", x => new { x.FlowId, x.WOCategoryId });
                    table.ForeignKey(
                        name: "FK_FlowsWOCategories_Flows_FlowId",
                        column: x => x.FlowId,
                        principalTable: "Flows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowsWOCategories_WorkOrderCategories_WOCategoryId",
                        column: x => x.WOCategoryId,
                        principalTable: "WorkOrderCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowsProjects_ProjectId",
                table: "FlowsProjects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowsWOCategories_WOCategoryId",
                table: "FlowsWOCategories",
                column: "WOCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlowsProjects");

            migrationBuilder.DropTable(
                name: "FlowsWOCategories");
        }
    }
}
