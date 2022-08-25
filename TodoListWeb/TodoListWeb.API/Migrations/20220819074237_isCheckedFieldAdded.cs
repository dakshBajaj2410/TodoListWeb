using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoListWeb.API.Migrations
{
    public partial class isCheckedFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "TodoItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "TodoItems");
        }
    }
}
