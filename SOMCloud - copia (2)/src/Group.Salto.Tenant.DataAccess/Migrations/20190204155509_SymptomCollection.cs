using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class SymptomCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SymptomCollections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Element = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymptomCollections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SymptomCollectionSymptoms",
                columns: table => new
                {
                    SymptomId = table.Column<int>(nullable: false),
                    SymptomCollectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymptomCollectionSymptoms", x => new { x.SymptomId, x.SymptomCollectionId });
                    table.ForeignKey(
                        name: "FK_SymptomCollectionSymptoms_SymptomCollections",
                        column: x => x.SymptomCollectionId,
                        principalTable: "SymptomCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SymptomCollectionSymptoms_Symptoms",
                        column: x => x.SymptomId,
                        principalTable: "Symptoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SymptomCollectionSymptoms_SymptomCollectionId",
                table: "SymptomCollectionSymptoms",
                column: "SymptomCollectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SymptomCollectionSymptoms");

            migrationBuilder.DropTable(
                name: "SymptomCollections");
        }
    }
}
