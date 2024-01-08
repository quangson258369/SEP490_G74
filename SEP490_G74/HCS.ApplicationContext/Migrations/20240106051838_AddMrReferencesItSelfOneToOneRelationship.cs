using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCS.ApplicationContext.Migrations
{
    /// <inheritdoc />
    public partial class AddMrReferencesItSelfOneToOneRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreviousMedicalRecordId",
                table: "MedicalRecords",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "MedicalRecords",
                keyColumn: "MedicalRecordId",
                keyValue: 1,
                columns: new[] { "MedicalRecordDate", "PreviousMedicalRecordId" },
                values: new object[] { new DateTime(2024, 1, 6, 12, 18, 37, 583, DateTimeKind.Local).AddTicks(5585), null });

            migrationBuilder.UpdateData(
                table: "MedicalRecords",
                keyColumn: "MedicalRecordId",
                keyValue: 2,
                columns: new[] { "MedicalRecordDate", "PreviousMedicalRecordId" },
                values: new object[] { new DateTime(2024, 1, 6, 12, 18, 37, 583, DateTimeKind.Local).AddTicks(5598), null });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PreviousMedicalRecordId",
                table: "MedicalRecords",
                column: "PreviousMedicalRecordId",
                unique: true,
                filter: "[PreviousMedicalRecordId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_MedicalRecords_PreviousMedicalRecordId",
                table: "MedicalRecords",
                column: "PreviousMedicalRecordId",
                principalTable: "MedicalRecords",
                principalColumn: "MedicalRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_MedicalRecords_PreviousMedicalRecordId",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_PreviousMedicalRecordId",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "PreviousMedicalRecordId",
                table: "MedicalRecords");

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
    }
}
