using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureMigrationsSample.Web.Migrations
{
    public partial class Add_value : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Chamber",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Chamber");
        }
    }
}
