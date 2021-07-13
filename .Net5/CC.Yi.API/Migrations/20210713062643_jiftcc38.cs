using Microsoft.EntityFrameworkCore.Migrations;

namespace CC.Yi.API.Migrations
{
    public partial class jiftcc38 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "discussid",
                table: "record",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_record_discussid",
                table: "record",
                column: "discussid");

            migrationBuilder.AddForeignKey(
                name: "FK_record_discuss_discussid",
                table: "record",
                column: "discussid",
                principalTable: "discuss",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_record_discuss_discussid",
                table: "record");

            migrationBuilder.DropIndex(
                name: "IX_record_discussid",
                table: "record");

            migrationBuilder.DropColumn(
                name: "discussid",
                table: "record");
        }
    }
}
