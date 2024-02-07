using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_None_Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "category",
                table: "categories",
                columns: new[] { "id", "photo_id" },
                values: new object[] { new Guid("c82e0550-26cf-410d-8cec-5cf62bada757"), null });

            migrationBuilder.InsertData(
                schema: "category",
                table: "category_translations",
                columns: new[] { "id", "category_id", "language_id", "name" },
                values: new object[,]
                {
                    { new Guid("206a3c95-fb6d-4127-a37b-9f328c021021"), new Guid("c82e0550-26cf-410d-8cec-5cf62bada757"), new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Keine" },
                    { new Guid("abed62c9-41b4-462f-866b-06d714dec958"), new Guid("c82e0550-26cf-410d-8cec-5cf62bada757"), new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Brak" },
                    { new Guid("f7b5e10f-0719-4731-831a-ffe0a1a1ed07"), new Guid("c82e0550-26cf-410d-8cec-5cf62bada757"), new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "None" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("206a3c95-fb6d-4127-a37b-9f328c021021"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("abed62c9-41b4-462f-866b-06d714dec958"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("f7b5e10f-0719-4731-831a-ffe0a1a1ed07"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("c82e0550-26cf-410d-8cec-5cf62bada757"));
        }
    }
}
