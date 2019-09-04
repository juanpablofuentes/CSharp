using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class workcenters_CountryRegionStat_Ids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "WorkCenters",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "WorkCenters",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "WorkCenters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "WorkCenters");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "WorkCenters");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "WorkCenters");
        }
    }
}
