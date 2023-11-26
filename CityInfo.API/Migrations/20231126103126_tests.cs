using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.API.Migrations
{
    public partial class tests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointOfIntrest_test_TestId",
                table: "PointOfIntrest");

            migrationBuilder.AlterColumn<int>(
                name: "TestId",
                table: "PointOfIntrest",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_PointOfIntrest_test_TestId",
                table: "PointOfIntrest",
                column: "TestId",
                principalTable: "test",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointOfIntrest_test_TestId",
                table: "PointOfIntrest");

            migrationBuilder.AlterColumn<int>(
                name: "TestId",
                table: "PointOfIntrest",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PointOfIntrest_test_TestId",
                table: "PointOfIntrest",
                column: "TestId",
                principalTable: "test",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
