using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class AddTwoColumnsSla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinutesPenaltyWithoutResponse",
                table: "SLA",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MinutesPenaltyWithoutResponseNaturals",
                table: "SLA",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinutesPenaltyWithoutResponse",
                table: "SLA");

            migrationBuilder.DropColumn(
                name: "MinutesPenaltyWithoutResponseNaturals",
                table: "SLA");
        }
    }
}
