using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Electronic.Api.Migrations
{
    public partial class aprov : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FirstAprove",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstAprove",
                table: "Products");
        }
    }
}
