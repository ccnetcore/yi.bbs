using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CC.Yi.API.Migrations
{
    public partial class jiftcc31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "friend",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    user1id = table.Column<int>(type: "INTEGER", nullable: true),
                    user2id = table.Column<int>(type: "INTEGER", nullable: true),
                    time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    is_agree = table.Column<int>(type: "INTEGER", nullable: false),
                    is_delete = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friend", x => x.id);
                    table.ForeignKey(
                        name: "FK_friend_user_user1id",
                        column: x => x.user1id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_friend_user_user2id",
                        column: x => x.user2id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_friend_user1id",
                table: "friend",
                column: "user1id");

            migrationBuilder.CreateIndex(
                name: "IX_friend_user2id",
                table: "friend",
                column: "user2id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "friend");
        }
    }
}
