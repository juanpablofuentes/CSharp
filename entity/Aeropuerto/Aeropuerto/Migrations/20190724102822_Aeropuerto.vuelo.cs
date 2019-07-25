using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aeropuerto.Migrations
{
    public partial class Aeropuertovuelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vuelo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    IdAvion = table.Column<int>(nullable: false),
                    IdPiloto = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vuelo", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_Vuelo_Avion",
                        column: x => x.IdAvion,
                        principalTable: "Aviones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_Vuelo_Piloto",
                        column: x => x.IdPiloto,
                        principalTable: "Pilotos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vuelo_IdAvion",
                table: "Vuelo",
                column: "IdAvion");

            migrationBuilder.CreateIndex(
                name: "IX_Vuelo_IdPiloto",
                table: "Vuelo",
                column: "IdPiloto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vuelo");
        }
    }
}
