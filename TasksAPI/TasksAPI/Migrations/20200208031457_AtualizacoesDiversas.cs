using Microsoft.EntityFrameworkCore.Migrations;

namespace TasksAPI.Migrations
{
    public partial class AtualizacoesDiversas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserFullName",
                table: "AspNetUsers",
                newName: "FullName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AspNetUsers",
                newName: "UserFullName");
        }
    }
}
