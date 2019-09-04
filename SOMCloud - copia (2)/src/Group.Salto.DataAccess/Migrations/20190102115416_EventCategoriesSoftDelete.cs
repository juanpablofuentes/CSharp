using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Migrations
{
    public partial class EventCategoriesSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarEventCategories_AvailabilityCategories",
                table: "CalendarEventCategories");

            migrationBuilder.AlterColumn<int>(
                name: "AvailabilityCategoriesId",
                table: "CalendarEventCategories",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CalendarEventCategories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarEventCategories_AvailabilityCategories",
                table: "CalendarEventCategories",
                column: "AvailabilityCategoriesId",
                principalTable: "AvailabilityCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarEventCategories_AvailabilityCategories",
                table: "CalendarEventCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CalendarEventCategories");

            migrationBuilder.AlterColumn<int>(
                name: "AvailabilityCategoriesId",
                table: "CalendarEventCategories",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarEventCategories_AvailabilityCategories",
                table: "CalendarEventCategories",
                column: "AvailabilityCategoriesId",
                principalTable: "AvailabilityCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
