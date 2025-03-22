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
            migrationBuilder.DropForeignKey(
                name: "FK_MedicineInfos_Prescriptions_PrescriptionId",
                table: "MedicineInfos");

            migrationBuilder.AlterColumn<int>(
                name: "PrescriptionId",
                table: "MedicineInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineInfos_Prescriptions_PrescriptionId",
                table: "MedicineInfos",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicineInfos_Prescriptions_PrescriptionId",
                table: "MedicineInfos");

            migrationBuilder.AlterColumn<int>(
                name: "PrescriptionId",
                table: "MedicineInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineInfos_Prescriptions_PrescriptionId",
                table: "MedicineInfos",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id");
        }
    }
}
