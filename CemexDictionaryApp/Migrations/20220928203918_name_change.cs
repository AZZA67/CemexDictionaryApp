using Microsoft.EntityFrameworkCore.Migrations;

namespace CemexDictionaryApp.Migrations
{
    public partial class name_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "الأسم",
                table: "QuestionCategories",
                newName: "Name_En");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "QuestionCategories",
                newName: "Name_Ar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name_En",
                table: "QuestionCategories",
                newName: "الأسم");

            migrationBuilder.RenameColumn(
                name: "Name_Ar",
                table: "QuestionCategories",
                newName: "Name");
        }
    }
}
