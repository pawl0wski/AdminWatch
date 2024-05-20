using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminWatchServer.Migrations
{
    /// <inheritdoc />
    public partial class ApprovedBySuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ApprovedBySuperAdmin",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBySuperAdmin",
                table: "AspNetUsers");
        }
    }
}
