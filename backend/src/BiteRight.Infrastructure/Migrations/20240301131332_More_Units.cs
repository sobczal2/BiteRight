using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class More_Units : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "unit",
                table: "units",
                columns: new[] { "id", "unit_system" },
                values: new object[,]
                {
                    { new Guid("84770558-3345-4d05-9814-21c61fbdbb09"), 1 },
                    { new Guid("f13143db-0380-4b90-8b8d-aad8c9ea7d48"), 1 },
                    { new Guid("f8920b2a-d1ee-4351-a708-4b82584257f1"), 3 }
                });

            migrationBuilder.InsertData(
                schema: "unit",
                table: "unit_translations",
                columns: new[] { "id", "abbreviation", "language_id", "name", "unit_id" },
                values: new object[,]
                {
                    { new Guid("0164a42a-a4b7-4e1f-8db8-8158b4605dbc"), "g", new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Gram", new Guid("84770558-3345-4d05-9814-21c61fbdbb09") },
                    { new Guid("256783e0-d6ba-4a6f-b0cc-52e3b908820c"), "szt", new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Sztuka", new Guid("f8920b2a-d1ee-4351-a708-4b82584257f1") },
                    { new Guid("3bf5d07a-6004-4f50-9bc4-59e8831f5e90"), "ml", new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Milliliter", new Guid("f13143db-0380-4b90-8b8d-aad8c9ea7d48") },
                    { new Guid("67cefc87-b43a-4262-9d16-9e12aac5db5e"), "ml", new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Mililitr", new Guid("f13143db-0380-4b90-8b8d-aad8c9ea7d48") },
                    { new Guid("831090a9-1736-4264-93fb-449aa3d3d02d"), "st", new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Stück", new Guid("f8920b2a-d1ee-4351-a708-4b82584257f1") },
                    { new Guid("b6101a50-6f20-42cf-b58d-1a69d77ffdfc"), "g", new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Gramm", new Guid("84770558-3345-4d05-9814-21c61fbdbb09") },
                    { new Guid("c9194190-521f-40ba-836f-6a8daccb418e"), "pc", new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Piece", new Guid("f8920b2a-d1ee-4351-a708-4b82584257f1") },
                    { new Guid("ca64ba08-93bb-432d-afc2-869f6c543922"), "g", new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Gram", new Guid("84770558-3345-4d05-9814-21c61fbdbb09") },
                    { new Guid("dc8020fa-c5c1-4e88-8658-752e5a922c3d"), "ml", new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Milliliter", new Guid("f13143db-0380-4b90-8b8d-aad8c9ea7d48") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("0164a42a-a4b7-4e1f-8db8-8158b4605dbc"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("256783e0-d6ba-4a6f-b0cc-52e3b908820c"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("3bf5d07a-6004-4f50-9bc4-59e8831f5e90"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("67cefc87-b43a-4262-9d16-9e12aac5db5e"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("831090a9-1736-4264-93fb-449aa3d3d02d"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("b6101a50-6f20-42cf-b58d-1a69d77ffdfc"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("c9194190-521f-40ba-836f-6a8daccb418e"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("ca64ba08-93bb-432d-afc2-869f6c543922"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "unit_translations",
                keyColumn: "id",
                keyValue: new Guid("dc8020fa-c5c1-4e88-8658-752e5a922c3d"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "units",
                keyColumn: "id",
                keyValue: new Guid("84770558-3345-4d05-9814-21c61fbdbb09"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "units",
                keyColumn: "id",
                keyValue: new Guid("f13143db-0380-4b90-8b8d-aad8c9ea7d48"));

            migrationBuilder.DeleteData(
                schema: "unit",
                table: "units",
                keyColumn: "id",
                keyValue: new Guid("f8920b2a-d1ee-4351-a708-4b82584257f1"));
        }
    }
}
