using Microsoft.EntityFrameworkCore.Migrations;

namespace CC.Yi.API.Migrations
{
    public partial class jiftcc27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "color",
                table: "discuss",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "encryption",
                table: "discuss",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "is_anonymous",
                table: "discuss",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "is_top",
                table: "discuss",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "color",
                table: "discuss");

            migrationBuilder.DropColumn(
                name: "encryption",
                table: "discuss");

            migrationBuilder.DropColumn(
                name: "is_anonymous",
                table: "discuss");

            migrationBuilder.DropColumn(
                name: "is_top",
                table: "discuss");
        }
    }
}
