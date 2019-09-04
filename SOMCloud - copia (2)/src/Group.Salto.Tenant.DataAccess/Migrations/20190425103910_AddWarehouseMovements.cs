using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class AddWarehouseMovements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WarehouseMovements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ItemsId = table.Column<int>(nullable: false),
                    WarehouseMovementTypeId = table.Column<Guid>(nullable: false),
                    WorkOrdersId = table.Column<int>(nullable: true),
                    ServicesId = table.Column<int>(nullable: true),
                    SerialNumber = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    EndpointsFromId = table.Column<int>(nullable: false),
                    EndpointsToId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseMovements_WarehouseMovementEndpointsFrom",
                        column: x => x.EndpointsFromId,
                        principalTable: "WarehouseMovementEndpoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarehouseMovements_WarehouseMovementEndpointsTo",
                        column: x => x.EndpointsToId,
                        principalTable: "WarehouseMovementEndpoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarehouseMovements_Items",
                        column: x => x.ItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarehouseMovements_Services",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarehouseMovements_WorkOrders",
                        column: x => x.WorkOrdersId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarehouseMovements_ItemsSerialNumber",
                        columns: x => new { x.ItemsId, x.SerialNumber },
                        principalTable: "ItemsSerialNumber",
                        principalColumns: new[] { "ItemId", "SerialNumber" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMovements_EndpointsFromId",
                table: "WarehouseMovements",
                column: "EndpointsFromId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMovements_EndpointsToId",
                table: "WarehouseMovements",
                column: "EndpointsToId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMovements_ServicesId",
                table: "WarehouseMovements",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMovements_WorkOrdersId",
                table: "WarehouseMovements",
                column: "WorkOrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMovements_ItemsId_SerialNumber",
                table: "WarehouseMovements",
                columns: new[] { "ItemsId", "SerialNumber" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarehouseMovements");
        }
    }
}
