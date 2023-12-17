using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Noted.Migrations
{
    public partial class fix3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checklists_Colors_ColorId",
                table: "Checklists");

            migrationBuilder.DropIndex(
                name: "IX_Checklists_ColorId",
                table: "Checklists");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Checklists");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Checklists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Checklists");

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Checklists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_ColorId",
                table: "Checklists",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checklists_Colors_ColorId",
                table: "Checklists",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
