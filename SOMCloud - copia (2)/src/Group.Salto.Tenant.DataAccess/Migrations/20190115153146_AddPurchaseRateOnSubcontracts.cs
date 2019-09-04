using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class AddPurchaseRateOnSubcontracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchaseRateId",
                table: "SubContracts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubContracts_PurchaseRateId",
                table: "SubContracts",
                column: "PurchaseRateId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubContracts_PurchaseRate_PurchaseRateId",
                table: "SubContracts",
                column: "PurchaseRateId",
                principalTable: "PurchaseRate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubContracts_PurchaseRate_PurchaseRateId",
                table: "SubContracts");

            migrationBuilder.DropIndex(
                name: "IX_SubContracts_PurchaseRateId",
                table: "SubContracts");

            migrationBuilder.DropColumn(
                name: "PurchaseRateId",
                table: "SubContracts");
        }
    }
}
