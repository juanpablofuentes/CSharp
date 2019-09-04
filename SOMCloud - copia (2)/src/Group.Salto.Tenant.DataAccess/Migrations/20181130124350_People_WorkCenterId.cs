using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class People_WorkCenterId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkCenterId",
                table: "People",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_WorkCenterId",
                table: "People",
                column: "WorkCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK__People__WorkCenter",
                table: "People",
                column: "WorkCenterId",
                principalTable: "WorkCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__People__WorkCenter",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_WorkCenterId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "WorkCenterId",
                table: "People");
        }
    }
}
