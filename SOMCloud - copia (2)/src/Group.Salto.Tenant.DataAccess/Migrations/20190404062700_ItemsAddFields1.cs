using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class ItemsAddFields1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InDepot",
                table: "Items",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "InternalReference",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Items",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubFamiliesId",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TrackBySerialNumber",
                table: "Items",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Items_SubFamiliesId",
                table: "Items",
                column: "SubFamiliesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_SubFamilies",
                table: "Items",
                column: "SubFamiliesId",
                principalTable: "SubFamilies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_SubFamilies",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_SubFamiliesId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "InDepot",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "InternalReference",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SubFamiliesId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TrackBySerialNumber",
                table: "Items");
        }
    }
}
