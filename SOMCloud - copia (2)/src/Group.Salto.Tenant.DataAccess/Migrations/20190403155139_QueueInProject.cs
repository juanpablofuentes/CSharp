using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class QueueInProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QueuetId",
                table: "Projects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_QueuetId",
                table: "Projects",
                column: "QueuetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Queues",
                table: "Projects",
                column: "QueuetId",
                principalTable: "Queues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Queues",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_QueuetId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "QueuetId",
                table: "Projects");
        }
    }
}
