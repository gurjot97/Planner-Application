using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planner_Application.Migrations
{
    /// <inheritdoc />
    public partial class AddedQuoteProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Quote",
                table: "TaskDB",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quote",
                table: "EventDB",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quote",
                table: "TaskDB");

            migrationBuilder.DropColumn(
                name: "Quote",
                table: "EventDB");
        }
    }
}
