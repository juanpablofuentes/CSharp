using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class DropTable_RepetitionParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepetitionParameters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RepetitionParameters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Days = table.Column<int>(maxLength: 2, nullable: false, defaultValue: 30),
                    IdCalculationType = table.Column<int>(nullable: false, defaultValue: 1),
                    IdDamagedEquipment = table.Column<int>(nullable: false, defaultValue: 1),
                    IdDaysType = table.Column<int>(nullable: false, defaultValue: 1),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepetitionParameters", x => x.Id);
                });
        }
    }
}
