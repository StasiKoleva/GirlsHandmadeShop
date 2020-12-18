using Microsoft.EntityFrameworkCore.Migrations;

namespace GirlsHandmadeShop.Data.Migrations
{
    public partial class CartProductsChangeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCarts_AspNetUsers_ApplicationUserId",
                table: "ProductCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCarts_Carts_CartId",
                table: "ProductCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCarts_Products_ProductId",
                table: "ProductCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCarts",
                table: "ProductCarts");

            migrationBuilder.RenameTable(
                name: "ProductCarts",
                newName: "CartProducts");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCarts_CartId",
                table: "CartProducts",
                newName: "IX_CartProducts_CartId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCarts_ApplicationUserId",
                table: "CartProducts",
                newName: "IX_CartProducts_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartProducts",
                table: "CartProducts",
                columns: new[] { "ProductId", "CartId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_AspNetUsers_ApplicationUserId",
                table: "CartProducts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_Carts_CartId",
                table: "CartProducts",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_Products_ProductId",
                table: "CartProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_AspNetUsers_ApplicationUserId",
                table: "CartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_Carts_CartId",
                table: "CartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_Products_ProductId",
                table: "CartProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartProducts",
                table: "CartProducts");

            migrationBuilder.RenameTable(
                name: "CartProducts",
                newName: "ProductCarts");

            migrationBuilder.RenameIndex(
                name: "IX_CartProducts_CartId",
                table: "ProductCarts",
                newName: "IX_ProductCarts_CartId");

            migrationBuilder.RenameIndex(
                name: "IX_CartProducts_ApplicationUserId",
                table: "ProductCarts",
                newName: "IX_ProductCarts_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCarts",
                table: "ProductCarts",
                columns: new[] { "ProductId", "CartId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCarts_AspNetUsers_ApplicationUserId",
                table: "ProductCarts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCarts_Carts_CartId",
                table: "ProductCarts",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCarts_Products_ProductId",
                table: "ProductCarts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
