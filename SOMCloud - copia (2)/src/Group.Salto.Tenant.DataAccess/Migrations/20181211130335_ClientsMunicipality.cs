using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class ClientsMunicipality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "MunicipalityId",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostalCodeId",
                table: "Clients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MunicipalityId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PostalCodeId",
                table: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Clients",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Clients",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Clients",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Clients",
                unicode: false,
                maxLength: 100,
                nullable: true);
        }
    }
}
