using Microsoft.EntityFrameworkCore.Migrations;

namespace CC.Yi.API.Migrations
{
    public partial class jift2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_user_usersid",
                table: "comment");

            migrationBuilder.DropForeignKey(
                name: "FK_discuss_user_usersid",
                table: "discuss");

            migrationBuilder.RenameColumn(
                name: "usersid",
                table: "discuss",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_discuss_usersid",
                table: "discuss",
                newName: "IX_discuss_userid");

            migrationBuilder.RenameColumn(
                name: "usersid",
                table: "comment",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_comment_usersid",
                table: "comment",
                newName: "IX_comment_userid");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_user_userid",
                table: "comment",
                column: "userid",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_discuss_user_userid",
                table: "discuss",
                column: "userid",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_user_userid",
                table: "comment");

            migrationBuilder.DropForeignKey(
                name: "FK_discuss_user_userid",
                table: "discuss");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "discuss",
                newName: "usersid");

            migrationBuilder.RenameIndex(
                name: "IX_discuss_userid",
                table: "discuss",
                newName: "IX_discuss_usersid");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "comment",
                newName: "usersid");

            migrationBuilder.RenameIndex(
                name: "IX_comment_userid",
                table: "comment",
                newName: "IX_comment_usersid");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_user_usersid",
                table: "comment",
                column: "usersid",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_discuss_user_usersid",
                table: "discuss",
                column: "usersid",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
