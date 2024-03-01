using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABPBackendTZ.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeviceField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Devices",
                newName: "Token");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "Devices",
                newName: "Id");
        }
    }
}
