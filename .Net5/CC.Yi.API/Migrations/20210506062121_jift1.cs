using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CC.Yi.API.Migrations
{
    public partial class jift1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "action",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    action_name = table.Column<string>(type: "TEXT", nullable: true),
                    is_delete = table.Column<int>(type: "INTEGER", nullable: false),
                    router = table.Column<string>(type: "TEXT", nullable: true),
                    icon = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_action", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    role_name = table.Column<string>(type: "TEXT", nullable: true),
                    is_delete = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    time = table.Column<DateTime>(type: "TEXT", nullable: true),
                    is_delete = table.Column<int>(type: "INTEGER", nullable: false),
                    username = table.Column<string>(type: "TEXT", nullable: true),
                    password = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "actionrole",
                columns: table => new
                {
                    actionsid = table.Column<int>(type: "INTEGER", nullable: false),
                    rolesid = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actionrole", x => new { x.actionsid, x.rolesid });
                    table.ForeignKey(
                        name: "FK_actionrole_action_actionsid",
                        column: x => x.actionsid,
                        principalTable: "action",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_actionrole_role_rolesid",
                        column: x => x.rolesid,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "actionuser",
                columns: table => new
                {
                    actionsid = table.Column<int>(type: "INTEGER", nullable: false),
                    usersid = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actionuser", x => new { x.actionsid, x.usersid });
                    table.ForeignKey(
                        name: "FK_actionuser_action_actionsid",
                        column: x => x.actionsid,
                        principalTable: "action",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_actionuser_user_usersid",
                        column: x => x.usersid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "plate",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    logo = table.Column<string>(type: "TEXT", nullable: true),
                    introduction = table.Column<string>(type: "TEXT", nullable: true),
                    is_delete = table.Column<int>(type: "INTEGER", nullable: false),
                    userid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plate", x => x.id);
                    table.ForeignKey(
                        name: "FK_plate_user_userid",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "roleuser",
                columns: table => new
                {
                    rolesid = table.Column<int>(type: "INTEGER", nullable: false),
                    usersid = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roleuser", x => new { x.rolesid, x.usersid });
                    table.ForeignKey(
                        name: "FK_roleuser_role_rolesid",
                        column: x => x.rolesid,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_roleuser_user_usersid",
                        column: x => x.usersid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "discuss",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", nullable: true),
                    type = table.Column<string>(type: "TEXT", nullable: true),
                    introduction = table.Column<string>(type: "TEXT", nullable: true),
                    time = table.Column<DateTime>(type: "TEXT", nullable: true),
                    is_delete = table.Column<int>(type: "INTEGER", nullable: false),
                    content = table.Column<string>(type: "TEXT", nullable: true),
                    usersid = table.Column<int>(type: "INTEGER", nullable: true),
                    plateid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discuss", x => x.id);
                    table.ForeignKey(
                        name: "FK_discuss_plate_plateid",
                        column: x => x.plateid,
                        principalTable: "plate",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_discuss_user_usersid",
                        column: x => x.usersid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    time = table.Column<DateTime>(type: "TEXT", nullable: true),
                    is_delete = table.Column<int>(type: "INTEGER", nullable: false),
                    content = table.Column<string>(type: "TEXT", nullable: true),
                    usersid = table.Column<int>(type: "INTEGER", nullable: true),
                    discussid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment", x => x.id);
                    table.ForeignKey(
                        name: "FK_comment_discuss_discussid",
                        column: x => x.discussid,
                        principalTable: "discuss",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comment_user_usersid",
                        column: x => x.usersid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_actionrole_rolesid",
                table: "actionrole",
                column: "rolesid");

            migrationBuilder.CreateIndex(
                name: "IX_actionuser_usersid",
                table: "actionuser",
                column: "usersid");

            migrationBuilder.CreateIndex(
                name: "IX_comment_discussid",
                table: "comment",
                column: "discussid");

            migrationBuilder.CreateIndex(
                name: "IX_comment_usersid",
                table: "comment",
                column: "usersid");

            migrationBuilder.CreateIndex(
                name: "IX_discuss_plateid",
                table: "discuss",
                column: "plateid");

            migrationBuilder.CreateIndex(
                name: "IX_discuss_usersid",
                table: "discuss",
                column: "usersid");

            migrationBuilder.CreateIndex(
                name: "IX_plate_userid",
                table: "plate",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_roleuser_usersid",
                table: "roleuser",
                column: "usersid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "actionrole");

            migrationBuilder.DropTable(
                name: "actionuser");

            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "roleuser");

            migrationBuilder.DropTable(
                name: "action");

            migrationBuilder.DropTable(
                name: "discuss");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "plate");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
