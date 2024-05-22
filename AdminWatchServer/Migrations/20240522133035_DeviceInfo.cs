using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminWatchServer.Migrations
{
    /// <inheritdoc />
    public partial class DeviceInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ip",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "MacAdress",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "Os",
                table: "Devices",
                newName: "InfoId");

            migrationBuilder.CreateTable(
                name: "DeviceInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Os = table.Column<int>(type: "INTEGER", nullable: false),
                    Ip = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    MacAdress = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    ProcessorName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_InfoId",
                table: "Devices",
                column: "InfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceInfo_InfoId",
                table: "Devices",
                column: "InfoId",
                principalTable: "DeviceInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceInfo_InfoId",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "DeviceInfo");

            migrationBuilder.DropIndex(
                name: "IX_Devices_InfoId",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "InfoId",
                table: "Devices",
                newName: "Os");

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "Devices",
                type: "TEXT",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MacAdress",
                table: "Devices",
                type: "TEXT",
                maxLength: 11,
                nullable: false,
                defaultValue: "");
        }
    }
}
