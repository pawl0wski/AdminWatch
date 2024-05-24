using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminWatchServer.Migrations
{
    /// <inheritdoc />
    public partial class MoveTotalMemory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalMemory",
                table: "DeviceMemoryOccupy");

            migrationBuilder.AddColumn<double>(
                name: "TotalMemory",
                table: "DeviceInfos",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalMemory",
                table: "DeviceInfos");

            migrationBuilder.AddColumn<double>(
                name: "TotalMemory",
                table: "DeviceMemoryOccupy",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
