using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class RemoveSalesRateOnSubContracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__SubContra__Sales__1BE81D6E",
                table: "SubContracts");

            migrationBuilder.DropIndex(
                name: "IX_SubContracts_SalesRateId",
                table: "SubContracts");

            migrationBuilder.DropColumn(
                name: "SalesRateId",
                table: "SubContracts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalesRateId",
                table: "SubContracts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubContracts_SalesRateId",
                table: "SubContracts",
                column: "SalesRateId");

            migrationBuilder.AddForeignKey(
                name: "FK__SubContra__Sales__1BE81D6E",
                table: "SubContracts",
                column: "SalesRateId",
                principalTable: "SalesRate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
