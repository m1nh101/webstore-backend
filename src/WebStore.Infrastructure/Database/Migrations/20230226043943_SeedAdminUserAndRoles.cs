using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebStore.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUserAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5d4fe3c7-8a65-46d1-8de2-e05a0fb34d5e", "84e58e08-590f-4a55-beac-85266ef2ce2d", "admin", "ADMIN"},
                    { "7e7add3b-29c4-443c-9376-3ab37cb4a6a8", "70e51335-9381-4061-b853-092d492704a3", "customer", "CUSTOMER"}
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "56d77f8c-8c0c-4887-ac6d-98945c1c17b1", 0, "", "42139b77-acd4-49e9-b756-f1cae3ede9fa", "admin@admin.com", true, "", false, false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAEI+YWfE9tYgI/5CJP9GivO/bAUzN+drOmnx8IK0pJkGy/KjfNxgiMezTbJYU6uiM4A==", null, false, "abea88dc-a457-435c-9060-a4f3f22d8de3", false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5d4fe3c7-8a65-46d1-8de2-e05a0fb34d5e");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7e7add3b-29c4-443c-9376-3ab37cb4a6a8");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "56d77f8c-8c0c-4887-ac6d-98945c1c17b1");
        }
    }
}
