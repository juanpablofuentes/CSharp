using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Migrations
{
    public partial class Modify_Length_Customer_ConnString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ConnString",
                table: "Customers",
                unicode: false,
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 200);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ConnString",
                table: "Customers",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdateDate",
                value: new DateTime(2018, 10, 25, 10, 0, 47, 882, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdateDate",
                value: new DateTime(2018, 10, 25, 10, 0, 47, 882, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdateDate",
                value: new DateTime(2018, 10, 25, 10, 0, 47, 882, DateTimeKind.Utc));
        }
    }
}
