using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class FK_DnAndMaterialsAnalysis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DnAndMaterialsAnalysis_Bill",
                table: "DnAndMaterialsAnalysis",
                column: "Bill");

            migrationBuilder.CreateIndex(
                name: "IX_DnAndMaterialsAnalysis_ItemCode",
                table: "DnAndMaterialsAnalysis",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_DnAndMaterialsAnalysis_People",
                table: "DnAndMaterialsAnalysis",
                column: "People");

            migrationBuilder.AddForeignKey(
                name: "FK_DnAndMaterialsAnalysis_Bill",
                table: "DnAndMaterialsAnalysis",
                column: "Bill",
                principalTable: "Bill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DnAndMaterialsAnalysis_Item",
                table: "DnAndMaterialsAnalysis",
                column: "ItemCode",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DnAndMaterialsAnalysis_People",
                table: "DnAndMaterialsAnalysis",
                column: "People",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DnAndMaterialsAnalysis_Bill",
                table: "DnAndMaterialsAnalysis");

            migrationBuilder.DropForeignKey(
                name: "FK_DnAndMaterialsAnalysis_Item",
                table: "DnAndMaterialsAnalysis");

            migrationBuilder.DropForeignKey(
                name: "FK_DnAndMaterialsAnalysis_People",
                table: "DnAndMaterialsAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_DnAndMaterialsAnalysis_Bill",
                table: "DnAndMaterialsAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_DnAndMaterialsAnalysis_ItemCode",
                table: "DnAndMaterialsAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_DnAndMaterialsAnalysis_People",
                table: "DnAndMaterialsAnalysis");
        }
    }
}
