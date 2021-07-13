using Microsoft.EntityFrameworkCore.Migrations;

namespace CC.Yi.API.Migrations
{
    public partial class jiftcc33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "article",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    parentid = table.Column<int>(type: "INTEGER", nullable: false),
                    content = table.Column<string>(type: "TEXT", nullable: true),
                    discussid = table.Column<int>(type: "INTEGER", nullable: true),
                    is_delete = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article", x => x.id);
                    table.ForeignKey(
                        name: "FK_article_discuss_discussid",
                        column: x => x.discussid,
                        principalTable: "discuss",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_article_discussid",
                table: "article",
                column: "discussid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article");
        }
    }
}
