using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Guids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                schema: "category",
                table: "photos",
                columns: new[] { "id", "name" },
                values: new object[] { new Guid("4aa9576b-8f3d-4a09-a66e-caa3fddfb4fb"), "default.webp" });

            migrationBuilder.InsertData(
                schema: "unit",
                table: "unit_translations",
                columns: new[] { "id", "abbreviation", "language_id", "name", "unit_id" },
                values: new object[,]
                {
                    { new Guid("368fb52e-06d7-4b33-aceb-c7edfdf865ad"), "kg", new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Kilogramm", new Guid("cde52e6c-5d9d-4876-978a-bf67c02cc8be") },
                    { new Guid("712cd66c-87ac-4af4-bc07-2d42db04e8b3"), "kg", new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Kilogram", new Guid("cde52e6c-5d9d-4876-978a-bf67c02cc8be") },
                    { new Guid("ca5c1bac-a53d-44bf-9cdf-2cd165047f7b"), "L", new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Litr", new Guid("b6d4d4dd-c035-4047-b8ee-48937cb1f368") },
                    { new Guid("ce4f6636-140c-48f7-a2f4-3148045ca0e5"), "L", new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Liter", new Guid("b6d4d4dd-c035-4047-b8ee-48937cb1f368") },
                    { new Guid("d91f5a31-458e-4d08-bfee-1812183ea35d"), "L", new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Liter", new Guid("b6d4d4dd-c035-4047-b8ee-48937cb1f368") },
                    { new Guid("e45767ee-c69c-4c34-9729-ffa396306017"), "kg", new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Kilogram", new Guid("cde52e6c-5d9d-4876-978a-bf67c02cc8be") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "category",
                table: "photos",
                keyColumn: "id",
                keyValue: new Guid("4aa9576b-8f3d-4a09-a66e-caa3fddfb4fb"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("368fb52e-06d7-4b33-aceb-c7edfdf865ad"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("712cd66c-87ac-4af4-bc07-2d42db04e8b3"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("ca5c1bac-a53d-44bf-9cdf-2cd165047f7b"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("ce4f6636-140c-48f7-a2f4-3148045ca0e5"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("d91f5a31-458e-4d08-bfee-1812183ea35d"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("e45767ee-c69c-4c34-9729-ffa396306017"));

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
        }
    }
}
