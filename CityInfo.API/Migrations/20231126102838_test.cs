using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.API.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PointOfIntrest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false),
                    TestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointOfIntrest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointOfIntrest_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PointOfIntrestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test", x => x.Id);
                    table.ForeignKey(
                        name: "FK_test_PointOfIntrest_PointOfIntrestId",
                        column: x => x.PointOfIntrestId,
                        principalTable: "PointOfIntrest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PointOfIntrest_CityId",
                table: "PointOfIntrest",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_PointOfIntrest_TestId",
                table: "PointOfIntrest",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_test_PointOfIntrestId",
                table: "test",
                column: "PointOfIntrestId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointOfIntrest_test_TestId",
                table: "PointOfIntrest",
                column: "TestId",
                principalTable: "test",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointOfIntrest_Cities_CityId",
                table: "PointOfIntrest");

            migrationBuilder.DropForeignKey(
                name: "FK_PointOfIntrest_test_TestId",
                table: "PointOfIntrest");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "test");

            migrationBuilder.DropTable(
                name: "PointOfIntrest");
        }
    }
}
