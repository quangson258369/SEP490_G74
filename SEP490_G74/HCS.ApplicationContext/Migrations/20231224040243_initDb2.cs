using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HCS.ApplicationContext.Migrations
{
    /// <inheritdoc />
    public partial class initDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CategoryId", "Email", "Password", "RoleId", "Status" },
                values: new object[,]
                {
                    { 1, null, "vkhoa871@gmail.com", "d0c406e82877aacad00415ca64f821e9", 1, true },
                    { 2, 1, "sonnk1@gmail.com", "d0c406e82877aacad00415ca64f821e9", 2, true },
                    { 3, null, "yta1@gmail.com", "d0c406e82877aacad00415ca64f821e9", 4, true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
