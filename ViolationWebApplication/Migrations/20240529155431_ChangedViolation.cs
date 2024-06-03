using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViolationWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class ChangedViolation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Violations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Violations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
