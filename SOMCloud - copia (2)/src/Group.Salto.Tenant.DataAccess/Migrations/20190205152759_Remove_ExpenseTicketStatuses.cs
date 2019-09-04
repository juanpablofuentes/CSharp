using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class Remove_ExpenseTicketStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesTickets_ExpenseTicketStatuses_ExpenseTicketStatusId",
                table: "ExpensesTickets");

            migrationBuilder.DropTable(
                name: "ExpenseTicketStatuses");

            migrationBuilder.DropIndex(
                name: "IX_ExpensesTickets_ExpenseTicketStatusId",
                table: "ExpensesTickets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseTicketStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseTicketStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesTickets_ExpenseTicketStatusId",
                table: "ExpensesTickets",
                column: "ExpenseTicketStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesTickets_ExpenseTicketStatuses_ExpenseTicketStatusId",
                table: "ExpensesTickets",
                column: "ExpenseTicketStatusId",
                principalTable: "ExpenseTicketStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
