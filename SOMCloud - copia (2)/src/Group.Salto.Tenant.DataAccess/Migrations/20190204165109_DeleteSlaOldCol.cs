using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class DeleteSlaOldCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceMinutesPenaltyUnanswered",
                table: "SLA");

            migrationBuilder.DropColumn(
                name: "ReferenceMinutesPenaltyWithoutResolution",
                table: "SLA");

            migrationBuilder.DropColumn(
                name: "ReferenceMinutesResolution",
                table: "SLA");

            migrationBuilder.DropColumn(
                name: "ReferenceMinutesResponse",
                table: "SLA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferenceMinutesPenaltyUnanswered",
                table: "SLA",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('DataCreacio')");

            migrationBuilder.AddColumn<string>(
                name: "ReferenceMinutesPenaltyWithoutResolution",
                table: "SLA",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('DataCreacio')");

            migrationBuilder.AddColumn<string>(
                name: "ReferenceMinutesResolution",
                table: "SLA",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('DataCreacio')");

            migrationBuilder.AddColumn<string>(
                name: "ReferenceMinutesResponse",
                table: "SLA",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('DataCreacio')");
        }
    }
}
