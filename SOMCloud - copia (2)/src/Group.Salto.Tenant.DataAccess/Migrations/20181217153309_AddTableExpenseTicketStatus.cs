using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class AddTableExpenseTicketStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseTicketStatusId",
                table: "ExpensesTickets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExpenseTicketStatuses",
                columns: table => new
                {
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesTickets_ExpenseTicketStatuses_ExpenseTicketStatusId",
                table: "ExpensesTickets");

            migrationBuilder.DropTable(
                name: "ExpenseTicketStatuses");

            migrationBuilder.DropIndex(
                name: "IX_ExpensesTickets_ExpenseTicketStatusId",
                table: "ExpensesTickets");

            migrationBuilder.DropColumn(
                name: "ExpenseTicketStatusId",
                table: "ExpensesTickets");
        }
    }
}
