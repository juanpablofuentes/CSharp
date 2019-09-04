using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Migrations
{
    public partial class UpdateCustomerFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailNotifications",
                table: "Customers",
                newName: "AdministrativeEmail");

            migrationBuilder.AddColumn<string>(
                name: "AdministrativeFullName",
                table: "Customers",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DatabaseName",
                table: "Customers",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Customers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NIF",
                table: "Customers",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberTeamMembers",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TechnicalEmail",
                table: "Customers",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TechnicalFullName",
                table: "Customers",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateStatusDate",
                table: "Customers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DatabaseName",
                table: "Customers",
                column: "DatabaseName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Name",
                table: "Customers",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_DatabaseName",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Name",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AdministrativeFullName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DatabaseName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "NIF",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "NumberTeamMembers",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TechnicalEmail",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TechnicalFullName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UpdateStatusDate",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "AdministrativeEmail",
                table: "Customers",
                newName: "EmailNotifications");

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdateDate",
                value: new DateTime(2018, 10, 18, 13, 20, 38, 818, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdateDate",
                value: new DateTime(2018, 10, 18, 13, 20, 38, 818, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdateDate",
                value: new DateTime(2018, 10, 18, 13, 20, 38, 818, DateTimeKind.Utc));
        }
    }
}
