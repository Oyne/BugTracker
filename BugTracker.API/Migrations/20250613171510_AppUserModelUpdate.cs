using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.API.Migrations
{
    /// <inheritdoc />
    public partial class AppUserModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "AppUser",
                newName: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "AppUser",
                newName: "Username");
        }
    }
}
