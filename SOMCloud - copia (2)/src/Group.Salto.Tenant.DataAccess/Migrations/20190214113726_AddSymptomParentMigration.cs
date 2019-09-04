using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class AddSymptomParentMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SymptomFatherId",
                table: "Symptoms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_SymptomFatherId",
                table: "Symptoms",
                column: "SymptomFatherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Symptom_SymptomFather",
                table: "Symptoms",
                column: "SymptomFatherId",
                principalTable: "Symptoms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Symptom_SymptomFather",
                table: "Symptoms");

            migrationBuilder.DropIndex(
                name: "IX_Symptoms_SymptomFatherId",
                table: "Symptoms");

            migrationBuilder.DropColumn(
                name: "SymptomFatherId",
                table: "Symptoms");
        }
    }
}
