﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class WorkOrderStatusSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WorkOrderStatuses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WorkOrderStatuses");
        }
    }
}