using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Electronic.Api.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminReview",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "FirstAprove",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "FirstAprove",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AdminReview",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
