using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminWatchServer.Migrations
{
    /// <inheritdoc />
    public partial class DoubleOccupiedMemory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalMemory",
                table: "DeviceMemoryOccupy",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<double>(
                name: "OccupiedMemory",
                table: "DeviceMemoryOccupy",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TotalMemory",
                table: "DeviceMemoryOccupy",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<int>(
                name: "OccupiedMemory",
                table: "DeviceMemoryOccupy",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
