using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_tasks_2.Migrations
{
    /// <inheritdoc />
    public partial class repo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "states",
                table: "Departments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "states",
                table: "Departments");
        }
    }
}
