using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class Move_PreconditionTypeId_To_LiteralPrecondition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreconditionsTypeId",
                table: "Preconditions");

            migrationBuilder.AddColumn<Guid>(
                name: "PreconditionsTypeId",
                table: "LiteralsPreconditions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreconditionsTypeId",
                table: "LiteralsPreconditions");

            migrationBuilder.AddColumn<Guid>(
                name: "PreconditionsTypeId",
                table: "Preconditions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
