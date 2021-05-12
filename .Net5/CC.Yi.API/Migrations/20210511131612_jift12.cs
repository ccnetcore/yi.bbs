using Microsoft.EntityFrameworkCore.Migrations;

namespace CC.Yi.API.Migrations
{
    public partial class jift12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_label_discuss_discussid",
                table: "label");

            migrationBuilder.DropIndex(
                name: "IX_label_discussid",
                table: "label");

            migrationBuilder.DropColumn(
                name: "discussid",
                table: "label");

            migrationBuilder.CreateTable(
                name: "discusslabel",
                columns: table => new
                {
                    discussesid = table.Column<int>(type: "INTEGER", nullable: false),
                    labelsid = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discusslabel", x => new { x.discussesid, x.labelsid });
                    table.ForeignKey(
                        name: "FK_discusslabel_discuss_discussesid",
                        column: x => x.discussesid,
                        principalTable: "discuss",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_discusslabel_label_labelsid",
                        column: x => x.labelsid,
                        principalTable: "label",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_discusslabel_labelsid",
                table: "discusslabel",
                column: "labelsid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "discusslabel");

            migrationBuilder.AddColumn<int>(
                name: "discussid",
                table: "label",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_label_discussid",
                table: "label",
                column: "discussid");

            migrationBuilder.AddForeignKey(
                name: "FK_label_discuss_discussid",
                table: "label",
                column: "discussid",
                principalTable: "discuss",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
