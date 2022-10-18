using Microsoft.EntityFrameworkCore.Migrations;

namespace CemexDictionaryApp.Migrations
{
    public partial class de : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "customer_Questions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "customer_Questions");
        }
    }
}
