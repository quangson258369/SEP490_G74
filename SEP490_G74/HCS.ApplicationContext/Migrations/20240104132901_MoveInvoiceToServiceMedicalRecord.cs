using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCS.ApplicationContext.Migrations
{
    /// <inheritdoc />
    public partial class MoveInvoiceToServiceMedicalRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_MedicalRecords_MedicalRecordId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_MedicalRecordId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "MedicalRecordId",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "ServiceMedicalRecords",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "MedicalRecords",
                keyColumn: "MedicalRecordId",
                keyValue: 1,
                column: "MedicalRecordDate",
                value: new DateTime(2024, 1, 4, 20, 29, 0, 720, DateTimeKind.Local).AddTicks(2171));

            migrationBuilder.UpdateData(
                table: "MedicalRecords",
                keyColumn: "MedicalRecordId",
                keyValue: 2,
                column: "MedicalRecordDate",
                value: new DateTime(2024, 1, 4, 20, 29, 0, 720, DateTimeKind.Local).AddTicks(2182));

            migrationBuilder.CreateIndex(
                name: "IX_ServiceMedicalRecords_InvoiceId",
                table: "ServiceMedicalRecords",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceMedicalRecords_Invoices_InvoiceId",
                table: "ServiceMedicalRecords",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceMedicalRecords_Invoices_InvoiceId",
                table: "ServiceMedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_ServiceMedicalRecords_InvoiceId",
                table: "ServiceMedicalRecords");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "ServiceMedicalRecords");

            migrationBuilder.AddColumn<int>(
                name: "MedicalRecordId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "MedicalRecords",
                keyColumn: "MedicalRecordId",
                keyValue: 1,
                column: "MedicalRecordDate",
                value: new DateTime(2024, 1, 4, 12, 10, 45, 76, DateTimeKind.Local).AddTicks(5219));

            migrationBuilder.UpdateData(
                table: "MedicalRecords",
                keyColumn: "MedicalRecordId",
                keyValue: 2,
                column: "MedicalRecordDate",
                value: new DateTime(2024, 1, 4, 12, 10, 45, 76, DateTimeKind.Local).AddTicks(5230));

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_MedicalRecordId",
                table: "Invoices",
                column: "MedicalRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_MedicalRecords_MedicalRecordId",
                table: "Invoices",
                column: "MedicalRecordId",
                principalTable: "MedicalRecords",
                principalColumn: "MedicalRecordId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
