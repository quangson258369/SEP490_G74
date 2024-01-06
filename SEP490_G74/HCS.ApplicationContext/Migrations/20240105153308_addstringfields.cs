using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCS.ApplicationContext.Migrations
{
    /// <inheritdoc />
    public partial class addstringfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ServiceMedicalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Diagnose",
                table: "ServiceMedicalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "MedicalRecords",
                keyColumn: "MedicalRecordId",
                keyValue: 1,
                column: "MedicalRecordDate",
                value: new DateTime(2024, 1, 5, 22, 33, 7, 669, DateTimeKind.Local).AddTicks(8232));

            migrationBuilder.UpdateData(
                table: "MedicalRecords",
                keyColumn: "MedicalRecordId",
                keyValue: 2,
                column: "MedicalRecordDate",
                value: new DateTime(2024, 1, 5, 22, 33, 7, 669, DateTimeKind.Local).AddTicks(8250));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ServiceMedicalRecords");

            migrationBuilder.DropColumn(
                name: "Diagnose",
                table: "ServiceMedicalRecords");

            migrationBuilder.UpdateData(
                table: "MedicalRecords",
                keyColumn: "MedicalRecordId",
                keyValue: 1,
                column: "MedicalRecordDate",
                value: new DateTime(2024, 1, 4, 23, 39, 56, 580, DateTimeKind.Local).AddTicks(9041));

            migrationBuilder.UpdateData(
                table: "MedicalRecords",
                keyColumn: "MedicalRecordId",
                keyValue: 2,
                column: "MedicalRecordDate",
                value: new DateTime(2024, 1, 4, 23, 39, 56, 580, DateTimeKind.Local).AddTicks(9054));
        }
    }
}
