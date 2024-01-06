using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HCS.ApplicationContext.Migrations
{
    /// <inheritdoc />
    public partial class seeddataparient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "CId", "Address", "Dob", "Gender", "Img", "Name", "Phone" },
                values: new object[,]
                {
                    { 11, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "", "Benh nhan B", "" },
                    { 12, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "", "Benh nhan C", "" }
                });

            migrationBuilder.InsertData(
                table: "MedicalRecords",
                columns: new[] { "MedicalRecordId", "ExamReason", "ExaminationResultId", "IsCheckUp", "IsPaid", "MedicalRecordDate", "PatientId" },
                values: new object[] { 1, "patient 1 mr1", null, false, false, new DateTime(2024, 1, 4, 12, 10, 45, 76, DateTimeKind.Local).AddTicks(5219), 1 });

            migrationBuilder.InsertData(
                table: "MedicalRecordCategories",
                columns: new[] { "CategoryId", "MedicalRecordId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "MedicalRecordDoctors",
                columns: new[] { "DoctorId", "MedicalRecordId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "Allergieshistory", "BloodGroup", "BloodPressure", "ContactId", "Height", "ServiceDetailName", "Weight" },
                values: new object[,]
                {
                    { 2, "None", "A", (byte)128, 11, (byte)157, "None", (byte)50 },
                    { 3, "None", "A", (byte)128, 12, (byte)157, "None", (byte)50 }
                });

            migrationBuilder.InsertData(
                table: "MedicalRecords",
                columns: new[] { "MedicalRecordId", "ExamReason", "ExaminationResultId", "IsCheckUp", "IsPaid", "MedicalRecordDate", "PatientId" },
                values: new object[] { 2, "patient 2 mr2", null, false, false, new DateTime(2024, 1, 4, 12, 10, 45, 76, DateTimeKind.Local).AddTicks(5230), 2 });

            migrationBuilder.InsertData(
                table: "MedicalRecordCategories",
                columns: new[] { "CategoryId", "MedicalRecordId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "MedicalRecordDoctors",
                columns: new[] { "DoctorId", "MedicalRecordId" },
                values: new object[,]
                {
                    { 4, 2 },
                    { 6, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MedicalRecordCategories",
                keyColumns: new[] { "CategoryId", "MedicalRecordId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "MedicalRecordCategories",
                keyColumns: new[] { "CategoryId", "MedicalRecordId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "MedicalRecordCategories",
                keyColumns: new[] { "CategoryId", "MedicalRecordId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "MedicalRecordCategories",
                keyColumns: new[] { "CategoryId", "MedicalRecordId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "MedicalRecordDoctors",
                keyColumns: new[] { "DoctorId", "MedicalRecordId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "MedicalRecordDoctors",
                keyColumns: new[] { "DoctorId", "MedicalRecordId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "MedicalRecordDoctors",
                keyColumns: new[] { "DoctorId", "MedicalRecordId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "MedicalRecordDoctors",
                keyColumns: new[] { "DoctorId", "MedicalRecordId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MedicalRecords",
                keyColumn: "MedicalRecordId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MedicalRecords",
                keyColumn: "MedicalRecordId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 11);
        }
    }
}
