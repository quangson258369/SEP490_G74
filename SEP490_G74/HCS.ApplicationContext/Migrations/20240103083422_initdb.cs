using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HCS.ApplicationContext.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Contacts_ContactId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_ContactId",
                table: "Patients");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "Patients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Nội khoa" },
                    { 2, "Ngoại khoa" },
                    { 3, "Khoa thần kinh" }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "CId", "Address", "Dob", "Gender", "Img", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "", "Admin Khoa", "" },
                    { 2, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "", "Bsi Son", "" },
                    { 3, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "", "Bsi Bang", "" },
                    { 4, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "", "Bsi Tam", "" },
                    { 5, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "", "Y Ta Nho", "" },
                    { 6, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "", "Cashier Trinh", "" },
                    { 7, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "", "Bsi Banh", "" },
                    { 8, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "", "Bsi Vu", "" },
                    { 9, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "", "Bsi Van", "" },
                    { 10, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "", "Benh nhan A", "" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Doctor" },
                    { 3, "Cashier" },
                    { 4, "Nurse" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "Allergieshistory", "BloodGroup", "BloodPressure", "ContactId", "Height", "ServiceDetailName", "Weight" },
                values: new object[] { 1, "None", "A", (byte)128, 10, (byte)157, "None", (byte)50 });

            migrationBuilder.InsertData(
                table: "ServiceTypes",
                columns: new[] { "ServiceTypeId", "CategoryId", "ServiceTypeName" },
                values: new object[,]
                {
                    { 1, 1, "Khám tổng quát" },
                    { 2, 1, "Khám chuyên khoa" },
                    { 3, 2, "Khám nội soi" },
                    { 4, 2, "Chẩn đoán hình ảnh" },
                    { 5, 3, "Xét nghiệm" },
                    { 6, 3, "Phẫu thuật" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CategoryId", "ContactId", "Email", "Password", "RoleId", "Status" },
                values: new object[,]
                {
                    { 1, null, 1, "vkhoa871@gmail.com", "d0c406e82877aacad00415ca64f821e9", 1, true },
                    { 2, 1, 2, "sonnk1@gmail.com", "d0c406e82877aacad00415ca64f821e9", 2, true },
                    { 3, 2, 3, "doctor3@gmail.com", "d0c406e82877aacad00415ca64f821e9", 2, true },
                    { 4, 3, 4, "doctor4@gmail.com", "d0c406e82877aacad00415ca64f821e9", 2, true },
                    { 5, 3, 7, "doctor5@gmail.com", "d0c406e82877aacad00415ca64f821e9", 2, true },
                    { 6, 1, 8, "doctor6@gmail.com", "d0c406e82877aacad00415ca64f821e9", 2, true },
                    { 7, 2, 9, "doctor7@gmail.com", "d0c406e82877aacad00415ca64f821e9", 2, true },
                    { 8, null, 5, "yta1@gmail.com", "d0c406e82877aacad00415ca64f821e9", 4, true },
                    { 9, null, 6, "cashier1@gmail.com", "d0c406e82877aacad00415ca64f821e9", 3, true }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "Price", "ServiceName", "ServiceTypeId" },
                values: new object[,]
                {
                    { 1, 0.0, "Tiêm vaccine", 1 },
                    { 2, 0.0, "Khám mắt", 1 },
                    { 3, 0.0, "Khám đại tràng", 2 },
                    { 4, 0.0, "Siêu âm màu", 2 },
                    { 5, 0.0, "Xét nghiệm máu", 3 },
                    { 6, 0.0, "Phẫu thuật cắt ruột thừa", 3 },
                    { 7, 0.0, "Tiêm vaccine cúm", 4 },
                    { 8, 0.0, "Khám tai mũi họng", 4 },
                    { 9, 0.0, "Khám dạ dày", 5 },
                    { 10, 0.0, "Siêu âm", 5 },
                    { 11, 0.0, "Xét nghiệm nước tiểu", 6 },
                    { 12, 0.0, "Phẫu thuật nối gân tay", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ContactId",
                table: "Patients",
                column: "ContactId",
                unique: true,
                filter: "[ContactId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Contacts_ContactId",
                table: "Patients",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "CId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Contacts_ContactId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_ContactId",
                table: "Patients");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "CId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ContactId",
                table: "Patients",
                column: "ContactId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Contacts_ContactId",
                table: "Patients",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "CId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
