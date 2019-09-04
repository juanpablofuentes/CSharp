using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class Add_WorkOrders_GeneratorServiceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GeneratorServiceId",
                table: "WorkOrders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_GeneratorServiceId",
                table: "WorkOrders",
                column: "GeneratorServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Services",
                table: "WorkOrders",
                column: "GeneratorServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Services",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_GeneratorServiceId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "GeneratorServiceId",
                table: "WorkOrders");
        }
    }
}
