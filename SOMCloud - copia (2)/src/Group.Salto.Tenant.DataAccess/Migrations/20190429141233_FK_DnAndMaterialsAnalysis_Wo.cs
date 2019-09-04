using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class FK_DnAndMaterialsAnalysis_Wo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DnAndMaterialsAnalysis_WorkOrder",
                table: "DnAndMaterialsAnalysis",
                column: "WorkOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_DnAndMaterialsAnalysis_WorkOrder",
                table: "DnAndMaterialsAnalysis",
                column: "WorkOrder",
                principalTable: "WorkOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DnAndMaterialsAnalysis_WorkOrder",
                table: "DnAndMaterialsAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_DnAndMaterialsAnalysis_WorkOrder",
                table: "DnAndMaterialsAnalysis");
        }
    }
}
