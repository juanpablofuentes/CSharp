using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class ADD_ExtraFieldsTranslations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtraFieldsTranslations",
                columns: table => new
                {
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    NameText = table.Column<string>(nullable: true),
                    DescriptionText = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false),
                    ExtraFieldsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraFieldsTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraFieldsTranslations_ExtraFields_ExtraFieldsId",
                        column: x => x.ExtraFieldsId,
                        principalTable: "ExtraFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraFieldsTranslations_ExtraFieldsId",
                table: "ExtraFieldsTranslations",
                column: "ExtraFieldsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtraFieldsTranslations");
        }
    }
}
