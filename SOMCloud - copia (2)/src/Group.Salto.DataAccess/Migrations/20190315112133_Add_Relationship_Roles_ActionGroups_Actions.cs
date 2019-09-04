using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Migrations
{
    public partial class Add_Relationship_Roles_ActionGroups_Actions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RolesActionGroupsActions",
                columns: table => new
                {
                    RolId = table.Column<string>(nullable: false),
                    ActionGroupId = table.Column<Guid>(nullable: false),
                    ActionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesActionGroupsActions", x => new { x.RolId, x.ActionGroupId, x.ActionId });
                    table.ForeignKey(
                        name: "FK_RolesActionGroupsActions_ActionGroups_ActionGroupId",
                        column: x => x.ActionGroupId,
                        principalTable: "ActionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesActionGroupsActions_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesActionGroupsActions_AspNetRoles_RolId",
                        column: x => x.RolId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolesActionGroupsActions_ActionGroupId",
                table: "RolesActionGroupsActions",
                column: "ActionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesActionGroupsActions_ActionId",
                table: "RolesActionGroupsActions",
                column: "ActionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolesActionGroupsActions");
        }
    }
}
