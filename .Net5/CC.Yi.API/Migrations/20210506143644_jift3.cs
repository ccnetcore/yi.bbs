using Microsoft.EntityFrameworkCore.Migrations;

namespace CC.Yi.API.Migrations
{
    public partial class jift3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_extraid",
                table: "user",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "user_extra",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    experience = table.Column<int>(type: "INTEGER", nullable: false),
                    level = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_extra", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_user_extraid",
                table: "user",
                column: "user_extraid");

            migrationBuilder.AddForeignKey(
                name: "FK_user_user_extra_user_extraid",
                table: "user",
                column: "user_extraid",
                principalTable: "user_extra",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_user_extra_user_extraid",
                table: "user");

            migrationBuilder.DropTable(
                name: "user_extra");

            migrationBuilder.DropIndex(
                name: "IX_user_user_extraid",
                table: "user");

            migrationBuilder.DropColumn(
                name: "user_extraid",
                table: "user");
        }
    }
}
