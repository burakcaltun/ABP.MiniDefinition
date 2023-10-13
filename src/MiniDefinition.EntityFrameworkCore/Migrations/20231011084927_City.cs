using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniDefinition.Migrations
{
    /// <inheritdoc />
    public partial class City : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProcessID",
                table: "Cities",
                newName: "ProcessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProcessId",
                table: "Cities",
                newName: "ProcessID");
        }
    }
}
