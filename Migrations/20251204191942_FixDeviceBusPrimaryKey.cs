using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharp_bus_watcher_api.Migrations
{
    /// <inheritdoc />
    public partial class FixDeviceBusPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncidentReports_Devices_CreatedByDeviceId",
                table: "IncidentReports");

            migrationBuilder.DropIndex(
                name: "IX_IncidentReports_CreatedByDeviceId",
                table: "IncidentReports");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "DepartTime",
                table: "Buses",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DepartTime",
                table: "Buses",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Buses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentReports_CreatedByDeviceId",
                table: "IncidentReports",
                column: "CreatedByDeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncidentReports_Devices_CreatedByDeviceId",
                table: "IncidentReports",
                column: "CreatedByDeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
