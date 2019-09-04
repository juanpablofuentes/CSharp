using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Migrations
{
    public partial class Add_Fields_Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdministrativeFullName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TechnicalFullName",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "TechnicalEmail",
                table: "Customers",
                newName: "TechnicalAdministratorEmail");

            migrationBuilder.RenameColumn(
                name: "AdministrativeEmail",
                table: "Customers",
                newName: "InvoicingContactEmail");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerCode",
                table: "Customers",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoicingContactFirstSurname",
                table: "Customers",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoicingContactName",
                table: "Customers",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoicingContactSecondSurname",
                table: "Customers",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MunicipalityId",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberEmployees",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TechnicalAdministratorFirstSurname",
                table: "Customers",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechnicalAdministratorName",
                table: "Customers",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechnicalAdministratorSecondSurname",
                table: "Customers",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_MunicipalityId",
                table: "Customers",
                column: "MunicipalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Municipalities_MunicipalityId",
                table: "Customers",
                column: "MunicipalityId",
                principalTable: "Municipalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Municipalities_MunicipalityId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_MunicipalityId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "InvoicingContactFirstSurname",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "InvoicingContactName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "InvoicingContactSecondSurname",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MunicipalityId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "NumberEmployees",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TechnicalAdministratorFirstSurname",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TechnicalAdministratorName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TechnicalAdministratorSecondSurname",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "TechnicalAdministratorEmail",
                table: "Customers",
                newName: "TechnicalEmail");

            migrationBuilder.RenameColumn(
                name: "InvoicingContactEmail",
                table: "Customers",
                newName: "AdministrativeEmail");

            migrationBuilder.AddColumn<string>(
                name: "AdministrativeFullName",
                table: "Customers",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TechnicalFullName",
                table: "Customers",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
