using Microsoft.EntityFrameworkCore.Migrations;

namespace CC.Yi.API.Migrations
{
    public partial class jiftcc23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "introduction",
                table: "user_extra",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ip",
                table: "user",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "introduction",
                table: "user_extra");

            migrationBuilder.DropColumn(
                name: "ip",
                table: "user");
        }
    }
}
