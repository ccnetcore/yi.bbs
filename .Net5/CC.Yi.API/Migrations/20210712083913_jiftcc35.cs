using Microsoft.EntityFrameworkCore.Migrations;

namespace CC.Yi.API.Migrations
{
    public partial class jiftcc35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_article_article_parent_articleid",
                table: "article");

            migrationBuilder.RenameColumn(
                name: "parent_articleid",
                table: "article",
                newName: "articleid");

            migrationBuilder.RenameIndex(
                name: "IX_article_parent_articleid",
                table: "article",
                newName: "IX_article_articleid");

            migrationBuilder.AddForeignKey(
                name: "FK_article_article_articleid",
                table: "article",
                column: "articleid",
                principalTable: "article",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_article_article_articleid",
                table: "article");

            migrationBuilder.RenameColumn(
                name: "articleid",
                table: "article",
                newName: "parent_articleid");

            migrationBuilder.RenameIndex(
                name: "IX_article_articleid",
                table: "article",
                newName: "IX_article_parent_articleid");

            migrationBuilder.AddForeignKey(
                name: "FK_article_article_parent_articleid",
                table: "article",
                column: "parent_articleid",
                principalTable: "article",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
