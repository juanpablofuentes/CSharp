using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class _1605_Locations_LocationsFinalClients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fax",
                table: "LocationsFinalClients");

            migrationBuilder.DropColumn(
                name: "Phone1",
                table: "LocationsFinalClients");

            migrationBuilder.DropColumn(
                name: "Phone2",
                table: "LocationsFinalClients");

            migrationBuilder.DropColumn(
                name: "Phone3",
                table: "LocationsFinalClients");

            migrationBuilder.AlterColumn<string>(
                name: "CompositeCode",
                table: "LocationsFinalClients",
                unicode: false,
                maxLength: 101,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Locations",
                unicode: false,
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Locations");

            migrationBuilder.AlterColumn<string>(
                name: "CompositeCode",
                table: "LocationsFinalClients",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 101);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "LocationsFinalClients",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone1",
                table: "LocationsFinalClients",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone2",
                table: "LocationsFinalClients",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone3",
                table: "LocationsFinalClients",
                unicode: false,
                maxLength: 50,
                nullable: true);
        }
    }
}
