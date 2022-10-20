using Electronic.Api.Model.user;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Electronic.Api.Migrations
{
    public partial class roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
      table: "AspNetRoles",
      columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
      values: new object[] { Guid.NewGuid().ToString(), Enums.Roles.Admin.ToString(), Enums.Roles.Admin.ToString().ToUpper(), Guid.NewGuid().ToString() }
  );
            migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
            values: new object[] { Guid.NewGuid().ToString(), Enums.Roles.User.ToString(), Enums.Roles.User.ToString().ToUpper(), Guid.NewGuid().ToString() }
        );

            migrationBuilder.InsertData(
                     table: "AspNetRoles",
                     columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                     values: new object[] { Guid.NewGuid().ToString(), Enums.Roles.Seller.ToString(), Enums.Roles.Seller.ToString().ToUpper(), Guid.NewGuid().ToString() }
                 );



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [AspNetRoles]");
        }
    }
}
