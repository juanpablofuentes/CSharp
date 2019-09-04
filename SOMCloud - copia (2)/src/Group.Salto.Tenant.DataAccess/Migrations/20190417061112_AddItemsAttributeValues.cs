using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class AddItemsAttributeValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemsSerialNumberAttributeValues",
                columns: table => new
                {
                    ExtraFieldsId = table.Column<int>(nullable: false),
                    IntValue = table.Column<int>(nullable: true),
                    DecimalValue = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    DateTimeValue = table.Column<DateTime>(nullable: true),
                    StringValue = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ItemId = table.Column<int>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsSerialNumberAttributeValues", x => new { x.ItemId, x.SerialNumber, x.ExtraFieldsId });
                    table.ForeignKey(
                        name: "FK_ItemsSerialNumberAttributeValues_ExtraFields",
                        column: x => x.ExtraFieldsId,
                        principalTable: "ExtraFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemsSerialNumberAttributeValues_ItemsSerialNumber",
                        columns: x => new { x.ItemId, x.SerialNumber },
                        principalTable: "ItemsSerialNumber",
                        principalColumns: new[] { "ItemId", "SerialNumber" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemsSerialNumberAttributeValues_ExtraFieldsId",
                table: "ItemsSerialNumberAttributeValues",
                column: "ExtraFieldsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsSerialNumberAttributeValues");
        }
    }
}
