using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planner_Application.Migrations
{
    /// <inheritdoc />
    public partial class RemovedColorProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "EventDB");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "EventDB",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
