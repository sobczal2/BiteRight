using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class More_Currencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("e862f33f-a04a-4b4e-a4bb-9542b1db3eeb"),
                column: "symbol",
                value: "US$");

            migrationBuilder.InsertData(
                schema: "currency",
                table: "currencies",
                columns: new[] { "id", "iso4217code", "is_default", "name", "symbol" },
                values: new object[,]
                {
                    { new Guid("06fc7d4a-ac95-40e9-a685-0434fda14085"), "MXN", false, "Mexican peso", "$" },
                    { new Guid("0cf996fa-3f23-4209-9bad-367d21a1c79a"), "BRL", false, "Brazilian real", "R$" },
                    { new Guid("2bc7bbbc-881b-4460-a099-1d082d11b78b"), "NZD", false, "New Zealand dollar", "NZ$" },
                    { new Guid("5efc5c7c-872d-4559-a3f3-ecc53f8ae59e"), "AUD", false, "Australian dollar", "A$" },
                    { new Guid("776c1e32-3a9d-4e91-869f-81e89bc17465"), "JPY", false, "Japanese yen", "¥" },
                    { new Guid("9490768d-a205-4dcc-bb66-6a007f661b98"), "CNY", false, "Chinese yuan", "¥" },
                    { new Guid("9c32a5f5-dc05-4e83-ab12-d26bdd8d0663"), "SEK", false, "Swedish krona", "kr" },
                    { new Guid("9d2c8e61-f2be-43af-8939-8a0700ea7bda"), "INR", false, "Indian rupee", "₹" },
                    { new Guid("a353e300-89d7-47b0-9c55-f4c73ad12d98"), "KRW", false, "South Korean won", "₩" },
                    { new Guid("a418ccb9-82aa-4e69-963b-4736ff1e815a"), "CHF", false, "Swiss franc", "Fr" },
                    { new Guid("d502fd4e-77cc-4749-b8b0-cca05f6c7d1c"), "HKD", false, "Hong Kong dollar", "HK$" },
                    { new Guid("d9b9b44c-1731-4496-8aec-1eb6f32689f0"), "SGD", false, "Singapore dollar", "S$" },
                    { new Guid("dd1e997a-bef8-4f62-86ea-6f45bedcccb0"), "NOK", false, "Norwegian krone", "kr" },
                    { new Guid("f1b638fd-f37f-47c0-943e-a35f44edc2cc"), "TRY", false, "Turkish lira", "₺" },
                    { new Guid("f7e66c98-4dc8-43d8-b77b-8f9848a3888f"), "CAD", false, "Canadian dollar", "C$" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("06fc7d4a-ac95-40e9-a685-0434fda14085"));

            migrationBuilder.DeleteData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("0cf996fa-3f23-4209-9bad-367d21a1c79a"));

            migrationBuilder.DeleteData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("2bc7bbbc-881b-4460-a099-1d082d11b78b"));

            migrationBuilder.DeleteData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("5efc5c7c-872d-4559-a3f3-ecc53f8ae59e"));

            migrationBuilder.DeleteData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("776c1e32-3a9d-4e91-869f-81e89bc17465"));

            migrationBuilder.DeleteData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("9490768d-a205-4dcc-bb66-6a007f661b98"));

            migrationBuilder.DeleteData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("9c32a5f5-dc05-4e83-ab12-d26bdd8d0663"));

            migrationBuilder.DeleteData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("9d2c8e61-f2be-43af-8939-8a0700ea7bda"));

            migrationBuilder.DeleteData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("a353e300-89d7-47b0-9c55-f4c73ad12d98"));

            migrationBuilder.DeleteData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("a418ccb9-82aa-4e69-963b-4736ff1e815a"));

            migrationBuilder.DeleteData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("d502fd4e-77cc-4749-b8b0-cca05f6c7d1c"));

            migrationBuilder.DeleteData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("d9b9b44c-1731-4496-8aec-1eb6f32689f0"));

            migrationBuilder.DeleteData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("dd1e997a-bef8-4f62-86ea-6f45bedcccb0"));

            migrationBuilder.DeleteData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("f1b638fd-f37f-47c0-943e-a35f44edc2cc"));

            migrationBuilder.DeleteData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("f7e66c98-4dc8-43d8-b77b-8f9848a3888f"));

            migrationBuilder.UpdateData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("e862f33f-a04a-4b4e-a4bb-9542b1db3eeb"),
                column: "symbol",
                value: "$");
        }
    }
}
