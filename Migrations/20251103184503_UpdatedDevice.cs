using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharp_bus_watcher_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "Devices",
                newName: "HardwareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HardwareId",
                table: "Devices",
                newName: "DeviceId");
        }
    }
}
