using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WIRKDEVELOPER.Migrations
{
    public partial class nhy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PharmacyMedicationID",
                table: "PrescriptionMedicationViewModel");

            migrationBuilder.AddColumn<string>(
                name: "PharmacyMedicationName",
                table: "PrescriptionMedicationViewModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PharmacyMedicationName",
                table: "PrescriptionMedicationViewModel");

            migrationBuilder.AddColumn<int>(
                name: "PharmacyMedicationID",
                table: "PrescriptionMedicationViewModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
