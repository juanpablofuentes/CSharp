using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class ModifySlaNewCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReferenceMinutesResponse_New",
                table: "SLA",
                newName: "ReferenceMinutesResponse");

            migrationBuilder.RenameColumn(
                name: "ReferenceMinutesResolution_New",
                table: "SLA",
                newName: "ReferenceMinutesResolution");

            migrationBuilder.RenameColumn(
                name: "ReferenceMinutesPenaltyWithoutResolution_New",
                table: "SLA",
                newName: "ReferenceMinutesPenaltyWithoutResolution");

            migrationBuilder.RenameColumn(
                name: "ReferenceMinutesPenaltyUnanswered_New",
                table: "SLA",
                newName: "ReferenceMinutesPenaltyUnanswered");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReferenceMinutesResponse",
                table: "SLA",
                newName: "ReferenceMinutesResponse_New");

            migrationBuilder.RenameColumn(
                name: "ReferenceMinutesResolution",
                table: "SLA",
                newName: "ReferenceMinutesResolution_New");

            migrationBuilder.RenameColumn(
                name: "ReferenceMinutesPenaltyWithoutResolution",
                table: "SLA",
                newName: "ReferenceMinutesPenaltyWithoutResolution_New");

            migrationBuilder.RenameColumn(
                name: "ReferenceMinutesPenaltyUnanswered",
                table: "SLA",
                newName: "ReferenceMinutesPenaltyUnanswered_New");
        }
    }
}
