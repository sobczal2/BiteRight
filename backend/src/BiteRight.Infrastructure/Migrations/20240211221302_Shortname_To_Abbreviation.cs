using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Shortname_To_Abbreviation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("1622a17d-e814-4479-a36f-fd4f150baf3d"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("3ca1440b-7bb2-4cfc-99a2-480eaf9f80ce"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("4aa58fa5-983d-4421-8c31-b277eea738ff"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("62e0417f-c78c-4e4d-86cc-440e22a6694b"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("719f0522-a85a-49cf-82d9-30d76b804b1d"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("e9dafc35-4599-40bf-ae44-09432e7b3379"));

            migrationBuilder.RenameColumn(
                name: "short_name",
                schema: "unit",
                table: "unit_translations",
                newName: "abbreviation");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.RenameColumn(
                name: "abbreviation",
                schema: "unit",
                table: "unit_translations",
                newName: "short_name");

            migrationBuilder.InsertData(
                schema: "unit",
                table: "unit_translations",
                columns: new[] { "id", "language_id", "name", "short_name", "unit_id" },
                values: new object[,]
                {
                    { new Guid("1622a17d-e814-4479-a36f-fd4f150baf3d"), new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Kilogramm", "kg", new Guid("cde52e6c-5d9d-4876-978a-bf67c02cc8be") },
                    { new Guid("3ca1440b-7bb2-4cfc-99a2-480eaf9f80ce"), new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Litr", "L", new Guid("b6d4d4dd-c035-4047-b8ee-48937cb1f368") },
                    { new Guid("4aa58fa5-983d-4421-8c31-b277eea738ff"), new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Liter", "L", new Guid("b6d4d4dd-c035-4047-b8ee-48937cb1f368") },
                    { new Guid("62e0417f-c78c-4e4d-86cc-440e22a6694b"), new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Kilogram", "kg", new Guid("cde52e6c-5d9d-4876-978a-bf67c02cc8be") },
                    { new Guid("719f0522-a85a-49cf-82d9-30d76b804b1d"), new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Kilogram", "kg", new Guid("cde52e6c-5d9d-4876-978a-bf67c02cc8be") },
                    { new Guid("e9dafc35-4599-40bf-ae44-09432e7b3379"), new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Liter", "L", new Guid("b6d4d4dd-c035-4047-b8ee-48937cb1f368") }
                });
        }
    }
}
