using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class Remove_UQ_ItemsSerialNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__ItemsSer__048A000827618D28",
                table: "ItemsSerialNumber");            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UQ__ItemsSer__048A000827618D28",
                table: "ItemsSerialNumber",
                column: "SerialNumber",
                unique: true);
        }
    }
}
