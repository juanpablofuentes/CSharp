using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Migrations
{
    public partial class AddedModulesAndCustomerAssociation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerModules",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(nullable: false),
                    ModuleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerModules", x => new { x.CustomerId, x.ModuleId });
                    table.ForeignKey(
                        name: "FK_CustomerModules_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerModules_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdateDate",
                value: new DateTime(2018, 10, 30, 9, 50, 35, 483, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdateDate",
                value: new DateTime(2018, 10, 30, 9, 50, 35, 483, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdateDate",
                value: new DateTime(2018, 10, 30, 9, 50, 35, 483, DateTimeKind.Utc));

            migrationBuilder.CreateIndex(
                name: "IX_CustomerModules_ModuleId",
                table: "CustomerModules",
                column: "ModuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerModules");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdateDate",
                value: new DateTime(2018, 10, 26, 11, 37, 12, 38, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdateDate",
                value: new DateTime(2018, 10, 26, 11, 37, 12, 38, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdateDate",
                value: new DateTime(2018, 10, 26, 11, 37, 12, 38, DateTimeKind.Utc));
        }
    }
}
