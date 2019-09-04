using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class WorkOrderCategoryRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkOrderCategoryRoles",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    WorkOrderCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderCategoryRoles", x => new { x.RoleId, x.WorkOrderCategoryId });
                    table.ForeignKey(
                        name: "FK_WorkOrderCategoryRoles_WorkOrderCategories_WorkOrderCategoryId",
                        column: x => x.WorkOrderCategoryId,
                        principalTable: "WorkOrderCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderCategoryRoles_WorkOrderCategoryId",
                table: "WorkOrderCategoryRoles",
                column: "WorkOrderCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkOrderCategoryRoles");
        }
    }
}
