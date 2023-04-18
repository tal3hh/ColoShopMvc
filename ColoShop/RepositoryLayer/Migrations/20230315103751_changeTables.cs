using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class changeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Genders_GenderID",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_GenderID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "GenderID",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "GenderID",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_GenderID",
                table: "Products",
                column: "GenderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Genders_GenderID",
                table: "Products",
                column: "GenderID",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Genders_GenderID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_GenderID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GenderID",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "GenderID",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_GenderID",
                table: "Categories",
                column: "GenderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Genders_GenderID",
                table: "Categories",
                column: "GenderID",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
