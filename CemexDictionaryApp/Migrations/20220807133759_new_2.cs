using Microsoft.EntityFrameworkCore.Migrations;

namespace CemexDictionaryApp.Migrations
{
    public partial class new_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_customer_Questions_QuestionCategories_CategoryId",
                table: "customer_Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerQuestionMedias_customer_Questions_QuestionId",
                table: "CustomerQuestionMedias");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsLog_news_NewId",
                table: "NewsLog");

            migrationBuilder.DropForeignKey(
                name: "FK_products_productTypes_ProductTypeId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsLog_products_ProductId",
                table: "ProductsLog");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionMedia_Questions_QuestionId",
                table: "QuestionMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_questionPerCategories_QuestionCategories_CategoryId",
                table: "questionPerCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_questionPerCategories_Questions_QuestionId",
                table: "questionPerCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_customer_Questions_QuestionCategories_CategoryId",
                table: "customer_Questions",
                column: "CategoryId",
                principalTable: "QuestionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerQuestionMedias_customer_Questions_QuestionId",
                table: "CustomerQuestionMedias",
                column: "QuestionId",
                principalTable: "customer_Questions",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsLog_news_NewId",
                table: "NewsLog",
                column: "NewId",
                principalTable: "news",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_products_productTypes_ProductTypeId",
                table: "products",
                column: "ProductTypeId",
                principalTable: "productTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsLog_products_ProductId",
                table: "ProductsLog",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionMedia_Questions_QuestionId",
                table: "QuestionMedia",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_questionPerCategories_QuestionCategories_CategoryId",
                table: "questionPerCategories",
                column: "CategoryId",
                principalTable: "QuestionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_questionPerCategories_Questions_QuestionId",
                table: "questionPerCategories",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_customer_Questions_QuestionCategories_CategoryId",
                table: "customer_Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerQuestionMedias_customer_Questions_QuestionId",
                table: "CustomerQuestionMedias");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsLog_news_NewId",
                table: "NewsLog");

            migrationBuilder.DropForeignKey(
                name: "FK_products_productTypes_ProductTypeId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsLog_products_ProductId",
                table: "ProductsLog");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionMedia_Questions_QuestionId",
                table: "QuestionMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_questionPerCategories_QuestionCategories_CategoryId",
                table: "questionPerCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_questionPerCategories_Questions_QuestionId",
                table: "questionPerCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_customer_Questions_QuestionCategories_CategoryId",
                table: "customer_Questions",
                column: "CategoryId",
                principalTable: "QuestionCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerQuestionMedias_customer_Questions_QuestionId",
                table: "CustomerQuestionMedias",
                column: "QuestionId",
                principalTable: "customer_Questions",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsLog_news_NewId",
                table: "NewsLog",
                column: "NewId",
                principalTable: "news",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_productTypes_ProductTypeId",
                table: "products",
                column: "ProductTypeId",
                principalTable: "productTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsLog_products_ProductId",
                table: "ProductsLog",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionMedia_Questions_QuestionId",
                table: "QuestionMedia",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_questionPerCategories_QuestionCategories_CategoryId",
                table: "questionPerCategories",
                column: "CategoryId",
                principalTable: "QuestionCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_questionPerCategories_Questions_QuestionId",
                table: "questionPerCategories",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "ID");
        }
    }
}
