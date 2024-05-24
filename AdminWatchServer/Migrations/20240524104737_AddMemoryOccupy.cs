using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminWatchServer.Migrations
{
    /// <inheritdoc />
    public partial class AddMemoryOccupy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceMemoryOccupy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OccupiedMemory = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalMemory = table.Column<int>(type: "INTEGER", nullable: false),
                    MeasureTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceMemoryOccupy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceMemoryOccupy_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceMemoryOccupy_DeviceId",
                table: "DeviceMemoryOccupy",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceMemoryOccupy");
        }
    }
}
