using Microsoft.EntityFrameworkCore.Migrations;

namespace GirlsHandmadeShop.Data.Migrations
{
    public partial class UserFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AddedByUserId",
                table: "Products",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_AddedByUserId",
                table: "Products",
                column: "AddedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_AddedByUserId",
                table: "Products",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_AddedByUserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_AddedByUserId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "AddedByUserId",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
