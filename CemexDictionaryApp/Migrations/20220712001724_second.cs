using Microsoft.EntityFrameworkCore.Migrations;

namespace CemexDictionaryApp.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsLog_news_newsId",
                table: "NewsLog");

            migrationBuilder.DropIndex(
                name: "IX_NewsLog_newsId",
                table: "NewsLog");

            migrationBuilder.DropColumn(
                name: "newsId",
                table: "NewsLog");

            migrationBuilder.CreateIndex(
                name: "IX_NewsLog_NewId",
                table: "NewsLog",
                column: "NewId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsLog_news_NewId",
                table: "NewsLog",
                column: "NewId",
                principalTable: "news",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsLog_news_NewId",
                table: "NewsLog");

            migrationBuilder.DropIndex(
                name: "IX_NewsLog_NewId",
                table: "NewsLog");

            migrationBuilder.AddColumn<int>(
                name: "newsId",
                table: "NewsLog",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsLog_newsId",
                table: "NewsLog",
                column: "newsId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsLog_news_newsId",
                table: "NewsLog",
                column: "newsId",
                principalTable: "news",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
