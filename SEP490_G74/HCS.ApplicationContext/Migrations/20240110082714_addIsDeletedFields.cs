using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCS.ApplicationContext.Migrations
{
    /// <inheritdoc />
    public partial class addIsDeletedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SuppliesTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Supplies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ServiceTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "MedicalRecords",
                keyColumn: "MedicalRecordId",
                keyValue: 1,
                column: "MedicalRecordDate",
                value: new DateTime(2024, 1, 10, 15, 27, 13, 618, DateTimeKind.Local).AddTicks(3899));

            migrationBuilder.UpdateData(
                table: "MedicalRecords",
                keyColumn: "MedicalRecordId",
                keyValue: 2,
                column: "MedicalRecordDate",
                value: new DateTime(2024, 1, 10, 15, 27, 13, 618, DateTimeKind.Local).AddTicks(3910));

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeId",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeId",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeId",
                keyValue: 3,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeId",
                keyValue: 4,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeId",
                keyValue: 5,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeId",
                keyValue: 6,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 3,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 4,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 5,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 6,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 7,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 8,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 9,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 10,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 11,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 12,
                column: "IsDeleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SuppliesTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "MedicalRecords",
                keyColumn: "MedicalRecordId",
                keyValue: 1,
                column: "MedicalRecordDate",
                value: new DateTime(2024, 1, 6, 12, 18, 37, 583, DateTimeKind.Local).AddTicks(5585));

            migrationBuilder.UpdateData(
                table: "MedicalRecords",
                keyColumn: "MedicalRecordId",
                keyValue: 2,
                column: "MedicalRecordDate",
                value: new DateTime(2024, 1, 6, 12, 18, 37, 583, DateTimeKind.Local).AddTicks(5598));
        }
    }
}
