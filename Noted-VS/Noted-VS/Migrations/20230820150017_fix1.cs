using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Noted.Migrations
{
    public partial class fix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeaderPhotoId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_HeaderPhotoId",
                table: "Users",
                column: "HeaderPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Photos_HeaderPhotoId",
                table: "Users",
                column: "HeaderPhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Photos_HeaderPhotoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_HeaderPhotoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HeaderPhotoId",
                table: "Users");
        }
    }
}
