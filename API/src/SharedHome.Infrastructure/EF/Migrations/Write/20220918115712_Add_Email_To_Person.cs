using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedHome.Infrastructure.EF.Migrations.Write
{
    public partial class Add_Email_To_Person : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Person",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Person");
        }
    }
}
