using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class ModifySlaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReferenceMinutesPenaltyUnanswered_New",
                table: "SLA",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReferenceMinutesPenaltyWithoutResolution_New",
                table: "SLA",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReferenceMinutesResolution_New",
                table: "SLA",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReferenceMinutesResponse_New",
                table: "SLA",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceMinutesPenaltyUnanswered_New",
                table: "SLA");

            migrationBuilder.DropColumn(
                name: "ReferenceMinutesPenaltyWithoutResolution_New",
                table: "SLA");

            migrationBuilder.DropColumn(
                name: "ReferenceMinutesResolution_New",
                table: "SLA");

            migrationBuilder.DropColumn(
                name: "ReferenceMinutesResponse_New",
                table: "SLA");
        }
    }
}
