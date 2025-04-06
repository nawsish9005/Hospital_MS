using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Migrations
{
    /// <inheritdoc />
    public partial class updatemedicine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From_Date",
                table: "MedicineInfos");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "MedicineInfos");

            migrationBuilder.RenameColumn(
                name: "Medicines",
                table: "MedicineInfos",
                newName: "MedicineName");

            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                table: "MedicineInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "MedicineInfos");

            migrationBuilder.RenameColumn(
                name: "MedicineName",
                table: "MedicineInfos",
                newName: "Medicines");

            migrationBuilder.AddColumn<DateTime>(
                name: "From_Date",
                table: "MedicineInfos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                table: "MedicineInfos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
