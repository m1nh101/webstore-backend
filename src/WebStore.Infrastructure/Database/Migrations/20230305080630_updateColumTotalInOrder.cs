using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebStore.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class updateColumTotalInOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9bbe6e4a-7ac2-4386-9752-e8e0128ec10e", "5cb5d084-04d5-446b-80dd-5ef61804340e", "customer", "CUSTOMER" },
                    { "f3fba1a7-4022-4db7-913f-f6439518cc99", "2424afd4-8dce-4516-adf3-4bc5871ba408", "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "83b376fe-3014-4522-84dc-2912ad00761c", 0, "", "18d1478b-a0a1-44a5-9413-0e1b92b9af70", "admin@admin.com", true, "", false, false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAEDJQ2DHSMORhVjHfIjcMbV7JhjBQjaKJ8/FGymrzkf2XEDw7fANHngJGnS15BTJTMg==", null, false, "c90668ec-c294-45f7-8ca6-fd4078dd7a71", false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9bbe6e4a-7ac2-4386-9752-e8e0128ec10e");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f3fba1a7-4022-4db7-913f-f6439518cc99");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "83b376fe-3014-4522-84dc-2912ad00761c");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5d4fe3c7-8a65-46d1-8de2-e05a0fb34d5e", null, "admin", "ADMIN" },
                    { "7e7add3b-29c4-443c-9376-3ab37cb4a6a8", null, "customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "56d77f8c-8c0c-4887-ac6d-98945c1c17b1", 0, "", "42139b77-acd4-49e9-b756-f1cae3ede9fa", "admin@admin.com", true, "", false, false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAEI+YWfE9tYgI/5CJP9GivO/bAUzN+drOmnx8IK0pJkGy/KjfNxgiMezTbJYU6uiM4A==", null, false, "abea88dc-a457-435c-9060-a4f3f22d8de3", false, "admin" });
        }
    }
}
