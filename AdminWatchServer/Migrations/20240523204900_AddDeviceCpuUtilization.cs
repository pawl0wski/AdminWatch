using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminWatchServer.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceCpuUtilization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceInfo_InfoId",
                table: "Devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceInfo",
                table: "DeviceInfo");

            migrationBuilder.RenameTable(
                name: "DeviceInfo",
                newName: "DeviceInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceInfos",
                table: "DeviceInfos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DeviceCpuUtilizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Utilization = table.Column<int>(type: "INTEGER", nullable: false),
                    MeasureTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCpuUtilizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceCpuUtilizations_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCpuUtilizations_DeviceId",
                table: "DeviceCpuUtilizations",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceInfos_InfoId",
                table: "Devices",
                column: "InfoId",
                principalTable: "DeviceInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceInfos_InfoId",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "DeviceCpuUtilizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceInfos",
                table: "DeviceInfos");

            migrationBuilder.RenameTable(
                name: "DeviceInfos",
                newName: "DeviceInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceInfo",
                table: "DeviceInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceInfo_InfoId",
                table: "Devices",
                column: "InfoId",
                principalTable: "DeviceInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
