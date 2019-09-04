using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Migrations
{
    public partial class WorkOrderColumnsAndDefaults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Align",
                table: "WorkOrderColumns",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CanSort",
                table: "WorkOrderColumns",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "WorkOrderColumns",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WorkOrderDefaultColumns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    WorkOrderColumnId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderDefaultColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK__WorkOrderColumns__WorkOrderDefaultColumns",
                        column: x => x.WorkOrderColumnId,
                        principalTable: "WorkOrderColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderDefaultColumns_WorkOrderColumnId",
                table: "WorkOrderDefaultColumns",
                column: "WorkOrderColumnId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkOrderDefaultColumns");

            migrationBuilder.DropColumn(
                name: "Align",
                table: "WorkOrderColumns");

            migrationBuilder.DropColumn(
                name: "CanSort",
                table: "WorkOrderColumns");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "WorkOrderColumns");
        }
    }
}
