using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class Add_relations_Tasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TasksTypesId",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TriggerTypesId",
                table: "Tasks",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TasksTypesId",
                table: "Tasks",
                column: "TasksTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskTypes",
                table: "Tasks",
                column: "TasksTypesId",
                principalTable: "TasksTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskTypes",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TasksTypesId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TasksTypesId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TriggerTypesId",
                table: "Tasks");
        }
    }
}
