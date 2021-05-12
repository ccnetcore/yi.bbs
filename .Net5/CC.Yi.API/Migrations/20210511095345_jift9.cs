using Microsoft.EntityFrameworkCore.Migrations;

namespace CC.Yi.API.Migrations
{
    public partial class jift9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "label",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    colour = table.Column<string>(type: "TEXT", nullable: true),
                    discussid = table.Column<int>(type: "INTEGER", nullable: true),
                    userid = table.Column<int>(type: "INTEGER", nullable: true),
                    is_delete = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_label", x => x.id);
                    table.ForeignKey(
                        name: "FK_label_discuss_discussid",
                        column: x => x.discussid,
                        principalTable: "discuss",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_label_user_userid",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_label_discussid",
                table: "label",
                column: "discussid");

            migrationBuilder.CreateIndex(
                name: "IX_label_userid",
                table: "label",
                column: "userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "label");
        }
    }
}
