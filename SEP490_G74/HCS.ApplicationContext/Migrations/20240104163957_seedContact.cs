using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCS.ApplicationContext.Migrations
{
    /// <inheritdoc />
    public partial class seedContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 1,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "Ha Noi", "0987662512" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 2,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "Ha Noi", "0987662512" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 3,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "Ha Noi", "0987662512" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 4,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "Ha Noi", "0987662512" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 5,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "Ha Noi", "0987662512" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 6,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "Ha Noi", "0987662512" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 7,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "Ha Noi", "0987662512" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 8,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "Ha Noi", "0987662512" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 9,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "Ha Noi", "0987662512" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 10,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "Ha Noi", "0987662512" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 11,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "Ha Noi", "0987662512" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 12,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "Ha Noi", "0987662512" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 1,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 2,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 3,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 4,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 5,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 6,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 7,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 8,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 9,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 10,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 11,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 12,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "", "" });

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
        }
    }
}
