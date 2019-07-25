using Microsoft.EntityFrameworkCore.Migrations;

namespace Aeropuerto.Migrations
{
    public partial class Aeropuertovuelos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vuelo_Aviones_IdAvion",
                table: "Vuelo");

            migrationBuilder.DropForeignKey(
                name: "ForeignKey_Vuelo_Avion",
                table: "Vuelo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vuelo",
                table: "Vuelo");

            migrationBuilder.RenameTable(
                name: "Vuelo",
                newName: "Vuelos");

            migrationBuilder.RenameIndex(
                name: "IX_Vuelo_IdPiloto",
                table: "Vuelos",
                newName: "IX_Vuelos_IdPiloto");

            migrationBuilder.RenameIndex(
                name: "IX_Vuelo_IdAvion",
                table: "Vuelos",
                newName: "IX_Vuelos_IdAvion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vuelos",
                table: "Vuelos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vuelos_Aviones_IdAvion",
                table: "Vuelos",
                column: "IdAvion",
                principalTable: "Aviones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_Vuelo_Piloto",
                table: "Vuelos",
                column: "IdPiloto",
                principalTable: "Pilotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vuelos_Aviones_IdAvion",
                table: "Vuelos");

            migrationBuilder.DropForeignKey(
                name: "ForeignKey_Vuelo_Piloto",
                table: "Vuelos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vuelos",
                table: "Vuelos");

            migrationBuilder.RenameTable(
                name: "Vuelos",
                newName: "Vuelo");

            migrationBuilder.RenameIndex(
                name: "IX_Vuelos_IdPiloto",
                table: "Vuelo",
                newName: "IX_Vuelo_IdPiloto");

            migrationBuilder.RenameIndex(
                name: "IX_Vuelos_IdAvion",
                table: "Vuelo",
                newName: "IX_Vuelo_IdAvion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vuelo",
                table: "Vuelo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vuelo_Aviones_IdAvion",
                table: "Vuelo",
                column: "IdAvion",
                principalTable: "Aviones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_Vuelo_Avion",
                table: "Vuelo",
                column: "IdPiloto",
                principalTable: "Pilotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
