using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class Change_Relation_WorkOrder_Service : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdreTreball_Services",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_ServiceId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "WorkOrders");

            migrationBuilder.CreateIndex(
                name: "IX_Services_WorkOrderId",
                table: "Services",
                column: "WorkOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Workorders",
                table: "Services",
                column: "WorkOrderId",
                principalTable: "WorkOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Workorders",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_WorkOrderId",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "WorkOrders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ServiceId",
                table: "WorkOrders",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdreTreball_Services",
                table: "WorkOrders",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
