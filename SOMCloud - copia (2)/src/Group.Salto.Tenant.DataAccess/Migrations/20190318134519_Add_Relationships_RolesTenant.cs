using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class Add_Relationships_RolesTenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RolesTenants",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesTenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolesActionGroupsActionsTenants",
                columns: table => new
                {
                    RoleTenantId = table.Column<string>(nullable: false),
                    ActionGroupId = table.Column<Guid>(nullable: false),
                    ActionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesActionGroupsActionsTenants", x => new { x.RoleTenantId, x.ActionGroupId, x.ActionId });
                    table.ForeignKey(
                        name: "FK_RolesActionGroupsActionsTenants_RolesTenants_RoleTenantId",
                        column: x => x.RoleTenantId,
                        principalTable: "RolesTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserConfigurationRolesTenants",
                columns: table => new
                {
                    UserConfigurationId = table.Column<int>(nullable: false),
                    RolesTenantId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConfigurationRolesTenants", x => new { x.UserConfigurationId, x.RolesTenantId });
                    table.ForeignKey(
                        name: "FK_UserConfigurationRolesTenants_RolesTenants_RolesTenantId",
                        column: x => x.RolesTenantId,
                        principalTable: "RolesTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserConfigurationRolesTenants_UserConfigurations_UserConfigurationId",
                        column: x => x.UserConfigurationId,
                        principalTable: "UserConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserConfigurationRolesTenants_RolesTenantId",
                table: "UserConfigurationRolesTenants",
                column: "RolesTenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolesActionGroupsActionsTenants");

            migrationBuilder.DropTable(
                name: "UserConfigurationRolesTenants");

            migrationBuilder.DropTable(
                name: "RolesTenants");
        }
    }
}
