using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedHome.Infrastructure.EF.Migrations
{
    public partial class Change_Product_Price_To_Money_And_Add_Net_Content : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "ShoppingListProduct",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NetContent",
                table: "ShoppingListProduct",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NetContentType",
                table: "ShoppingListProduct",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "ShoppingListProduct");

            migrationBuilder.DropColumn(
                name: "NetContent",
                table: "ShoppingListProduct");

            migrationBuilder.DropColumn(
                name: "NetContentType",
                table: "ShoppingListProduct");
        }
    }
}
