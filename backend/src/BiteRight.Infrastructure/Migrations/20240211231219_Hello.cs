using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Hello : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_profiles_profile_temp_id",
                schema: "user",
                table: "users");

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("19fcc278-0ae3-4ef4-8202-de7173d2c3b5"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("3a744dbe-3bd0-47e0-a8bc-154c9e12c9d5"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("70e1205f-a196-4b08-b247-04f7c24b5681"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("8f887391-8a4e-433b-9e74-119ded24be74"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("af18ee45-4e18-4d2f-9c1e-e9c24f7c20cd"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("dbb68edb-5350-49c2-8c93-5bad56eac063"));

            migrationBuilder.InsertData(
                schema: "unit",
                table: "unit_translations",
                columns: new[] { "id", "abbreviation", "language_id", "name", "unit_id" },
                values: new object[,]
                {
                    { new Guid("223c9c16-509f-454c-9d57-b4dbe46a9687"), "L", new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Litr", new Guid("b6d4d4dd-c035-4047-b8ee-48937cb1f368") },
                    { new Guid("6a436b72-bde9-4ef4-8df9-8cc0abe13115"), "L", new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Liter", new Guid("b6d4d4dd-c035-4047-b8ee-48937cb1f368") },
                    { new Guid("9125b867-044e-40c0-a1de-f8fed20f0476"), "kg", new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Kilogram", new Guid("cde52e6c-5d9d-4876-978a-bf67c02cc8be") },
                    { new Guid("b24bd84f-3294-4f2c-a711-a616c4effa17"), "kg", new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Kilogram", new Guid("cde52e6c-5d9d-4876-978a-bf67c02cc8be") },
                    { new Guid("e0b0ad1f-0f7b-4586-8afd-cabc85bb72c9"), "L", new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Liter", new Guid("b6d4d4dd-c035-4047-b8ee-48937cb1f368") },
                    { new Guid("f755089b-a0ee-4999-972d-77512fc9330c"), "kg", new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Kilogramm", new Guid("cde52e6c-5d9d-4876-978a-bf67c02cc8be") }
                });

            migrationBuilder.AddForeignKey(
                name: "fk_users_profiles_profile_id",
                schema: "user",
                table: "users",
                column: "profile_id",
                principalSchema: "user",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_profiles_profile_id",
                schema: "user",
                table: "users");

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("223c9c16-509f-454c-9d57-b4dbe46a9687"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("6a436b72-bde9-4ef4-8df9-8cc0abe13115"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("9125b867-044e-40c0-a1de-f8fed20f0476"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("b24bd84f-3294-4f2c-a711-a616c4effa17"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("e0b0ad1f-0f7b-4586-8afd-cabc85bb72c9"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("f755089b-a0ee-4999-972d-77512fc9330c"));

            migrationBuilder.InsertData(
                schema: "unit",
                table: "unit_translations",
                columns: new[] { "id", "abbreviation", "language_id", "name", "unit_id" },
                values: new object[,]
                {
                    { new Guid("19fcc278-0ae3-4ef4-8202-de7173d2c3b5"), "L", new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Liter", new Guid("b6d4d4dd-c035-4047-b8ee-48937cb1f368") },
                    { new Guid("3a744dbe-3bd0-47e0-a8bc-154c9e12c9d5"), "L", new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Litr", new Guid("b6d4d4dd-c035-4047-b8ee-48937cb1f368") },
                    { new Guid("70e1205f-a196-4b08-b247-04f7c24b5681"), "kg", new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Kilogramm", new Guid("cde52e6c-5d9d-4876-978a-bf67c02cc8be") },
                    { new Guid("8f887391-8a4e-433b-9e74-119ded24be74"), "kg", new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Kilogram", new Guid("cde52e6c-5d9d-4876-978a-bf67c02cc8be") },
                    { new Guid("af18ee45-4e18-4d2f-9c1e-e9c24f7c20cd"), "L", new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Liter", new Guid("b6d4d4dd-c035-4047-b8ee-48937cb1f368") },
                    { new Guid("dbb68edb-5350-49c2-8c93-5bad56eac063"), "kg", new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Kilogram", new Guid("cde52e6c-5d9d-4876-978a-bf67c02cc8be") }
                });

            migrationBuilder.AddForeignKey(
                name: "fk_users_profiles_profile_temp_id",
                schema: "user",
                table: "users",
                column: "profile_id",
                principalSchema: "user",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
