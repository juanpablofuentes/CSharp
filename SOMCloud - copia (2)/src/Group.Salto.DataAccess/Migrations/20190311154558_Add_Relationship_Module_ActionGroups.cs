using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Migrations
{
    public partial class Add_Relationship_Module_ActionGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModuleActionGroups",
                columns: table => new
                {
                    ModuleId = table.Column<Guid>(nullable: false),
                    ActionGroupsId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleActionGroups", x => new { x.ModuleId, x.ActionGroupsId });
                    table.ForeignKey(
                        name: "FK_ModuleActionGroups_ActionGroups_ActionGroupsId",
                        column: x => x.ActionGroupsId,
                        principalTable: "ActionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleActionGroups_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleActionGroups_ActionGroupsId",
                table: "ModuleActionGroups",
                column: "ActionGroupsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuleActionGroups");
        }
    }
}
