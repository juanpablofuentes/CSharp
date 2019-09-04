using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class AddItemSerialNumberStatuses_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemsSerialNumberStatusesId",
                table: "ItemsSerialNumber",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observations",
                table: "ItemsSerialNumber",
                nullable: true, maxLength: 500);

            migrationBuilder.CreateTable(
                name: "ItemsSerialNumberStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true, maxLength: 100)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsSerialNumberStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemsSerialNumber_ItemsSerialNumberStatusesId",
                table: "ItemsSerialNumber",
                column: "ItemsSerialNumberStatusesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsSerialNumber_ItemsSerialNumberStatuses",
                table: "ItemsSerialNumber",
                column: "ItemsSerialNumberStatusesId",
                principalTable: "ItemsSerialNumberStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsSerialNumber_ItemsSerialNumberStatuses",
                table: "ItemsSerialNumber");

            migrationBuilder.DropTable(
                name: "ItemsSerialNumberStatuses");

            migrationBuilder.DropIndex(
                name: "IX_ItemsSerialNumber_ItemsSerialNumberStatusesId",
                table: "ItemsSerialNumber");

            migrationBuilder.DropColumn(
                name: "ItemsSerialNumberStatusesId",
                table: "ItemsSerialNumber");

            migrationBuilder.DropColumn(
                name: "Observations",
                table: "ItemsSerialNumber");
        }
    }
}
