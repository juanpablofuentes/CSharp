using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Migrations
{
    public partial class WorkOrderColumnsNameString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UK_ColumnName",
                table: "WorkOrderColumns");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WorkOrderColumns",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldUnicode: false,
                oldMaxLength: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "WorkOrderColumns",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "UK_ColumnName",
                table: "WorkOrderColumns",
                column: "Name",
                unique: true);
        }
    }
}
