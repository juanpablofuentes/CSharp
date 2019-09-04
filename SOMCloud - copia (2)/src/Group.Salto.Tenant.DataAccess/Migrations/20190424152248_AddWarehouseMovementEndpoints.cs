using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class AddWarehouseMovementEndpoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WarehouseMovementEndpoints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    WarehouseId = table.Column<int>(nullable: true),
                    AssetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseMovementEndpoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseMovementEndpoints_Assets",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarehouseMovementEndpoints_Warehouses",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMovementEndpoints_AssetId",
                table: "WarehouseMovementEndpoints",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMovementEndpoints_WarehouseId",
                table: "WarehouseMovementEndpoints",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarehouseMovementEndpoints");
        }
    }
}
