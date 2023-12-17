using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Noted.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ProfilePhotos_ProfilePhotoId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfilePhotos",
                table: "ProfilePhotos");

            migrationBuilder.RenameTable(
                name: "ProfilePhotos",
                newName: "Photos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Photos_ProfilePhotoId",
                table: "Users",
                column: "ProfilePhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Photos_ProfilePhotoId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "ProfilePhotos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfilePhotos",
                table: "ProfilePhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ProfilePhotos_ProfilePhotoId",
                table: "Users",
                column: "ProfilePhotoId",
                principalTable: "ProfilePhotos",
                principalColumn: "Id");
        }
    }
}
