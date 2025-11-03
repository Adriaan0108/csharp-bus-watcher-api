using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharp_bus_watcher_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModelsLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_IncidentReports_BusId",
                table: "IncidentReports",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentReports_CreatedByDeviceId",
                table: "IncidentReports",
                column: "CreatedByDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentReportFeedbacks_CreatedByDeviceId",
                table: "IncidentReportFeedbacks",
                column: "CreatedByDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentReportFeedbacks_IncidentReportId",
                table: "IncidentReportFeedbacks",
                column: "IncidentReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncidentReportFeedbacks_Devices_CreatedByDeviceId",
                table: "IncidentReportFeedbacks",
                column: "CreatedByDeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IncidentReportFeedbacks_IncidentReports_IncidentReportId",
                table: "IncidentReportFeedbacks",
                column: "IncidentReportId",
                principalTable: "IncidentReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IncidentReports_Buses_BusId",
                table: "IncidentReports",
                column: "BusId",
                principalTable: "Buses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IncidentReports_Devices_CreatedByDeviceId",
                table: "IncidentReports",
                column: "CreatedByDeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncidentReportFeedbacks_Devices_CreatedByDeviceId",
                table: "IncidentReportFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_IncidentReportFeedbacks_IncidentReports_IncidentReportId",
                table: "IncidentReportFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_IncidentReports_Buses_BusId",
                table: "IncidentReports");

            migrationBuilder.DropForeignKey(
                name: "FK_IncidentReports_Devices_CreatedByDeviceId",
                table: "IncidentReports");

            migrationBuilder.DropIndex(
                name: "IX_IncidentReports_BusId",
                table: "IncidentReports");

            migrationBuilder.DropIndex(
                name: "IX_IncidentReports_CreatedByDeviceId",
                table: "IncidentReports");

            migrationBuilder.DropIndex(
                name: "IX_IncidentReportFeedbacks_CreatedByDeviceId",
                table: "IncidentReportFeedbacks");

            migrationBuilder.DropIndex(
                name: "IX_IncidentReportFeedbacks_IncidentReportId",
                table: "IncidentReportFeedbacks");
        }
    }
}
