using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CemexDictionaryApp.Migrations
{
    public partial class NewsDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SubmitTime",
                table: "news",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmitTime",
                table: "news");
        }
    }
}
