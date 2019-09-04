using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class ItemsInternalReference_Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InternalReference",
                table: "Items",
                nullable: true,
                maxLength: 200,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InternalReference",
                table: "Items",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
