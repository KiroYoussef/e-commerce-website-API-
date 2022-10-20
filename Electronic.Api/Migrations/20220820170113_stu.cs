using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Electronic.Api.Migrations
{
    public partial class stu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SatusId",
                table: "Orders",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SatusHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SatusHistories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SatusId",
                table: "Orders",
                column: "SatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_SatusHistories_SatusId",
                table: "Orders",
                column: "SatusId",
                principalTable: "SatusHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_SatusHistories_SatusId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "SatusHistories");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SatusId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SatusId",
                table: "Orders");
        }
    }
}
