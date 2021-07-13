using Microsoft.EntityFrameworkCore.Migrations;

namespace CC.Yi.API.Migrations
{
    public partial class jiftcc34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "parentid",
                table: "article");

            migrationBuilder.AddColumn<int>(
                name: "parent_articleid",
                table: "article",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_article_parent_articleid",
                table: "article",
                column: "parent_articleid");

            migrationBuilder.AddForeignKey(
                name: "FK_article_article_parent_articleid",
                table: "article",
                column: "parent_articleid",
                principalTable: "article",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_article_article_parent_articleid",
                table: "article");

            migrationBuilder.DropIndex(
                name: "IX_article_parent_articleid",
                table: "article");

            migrationBuilder.DropColumn(
                name: "parent_articleid",
                table: "article");

            migrationBuilder.AddColumn<int>(
                name: "parentid",
                table: "article",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
