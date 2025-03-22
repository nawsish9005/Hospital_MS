using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Migrations
{
    /// <inheritdoc />
    public partial class updateappointmentandmedical : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Consultation_Notes",
                table: "MedicalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Current_medications",
                table: "MedicalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FollowUp_Notes",
                table: "MedicalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrls",
                table: "MedicalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Laboratory_Results",
                table: "MedicalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Next_Visit_Date",
                table: "MedicalRecords",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentRecords",
                table: "MedicalRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Therapies",
                table: "MedicalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consultation_Notes",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "Current_medications",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "FollowUp_Notes",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "ImageUrls",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "Laboratory_Results",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "Next_Visit_Date",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "PaymentRecords",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "Therapies",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "Appointments");
        }
    }
}
