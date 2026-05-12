using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class MedicalRecordUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecordDate",
                table: "MedicalRecords",
                newName: "DateRecorded");

            migrationBuilder.RenameColumn(
                name: "PatientName",
                table: "MedicalRecords",
                newName: "FullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "MedicalRecords",
                newName: "PatientName");

            migrationBuilder.RenameColumn(
                name: "DateRecorded",
                table: "MedicalRecords",
                newName: "RecordDate");
        }
    }
}
