using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Migrations
{
    public partial class SetUniqueKeyModules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "Modules",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdateDate",
                value: new DateTime(2018, 10, 30, 13, 18, 41, 250, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdateDate",
                value: new DateTime(2018, 10, 30, 13, 18, 41, 250, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdateDate",
                value: new DateTime(2018, 10, 30, 13, 18, 41, 250, DateTimeKind.Utc));

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Key",
                table: "Modules",
                column: "Key",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Modules_Key",
                table: "Modules");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "Modules",
                nullable: false,
                oldClrType: typeof(string));

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
        }
    }
}
