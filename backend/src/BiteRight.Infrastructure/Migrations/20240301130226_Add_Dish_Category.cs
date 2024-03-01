using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Dish_Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "category",
                table: "photos",
                columns: new[] { "id", "name" },
                values: new object[] { new Guid("02f43b77-f3fc-4d97-a53c-a45fac195171"), "dish.webp" });

            migrationBuilder.InsertData(
                schema: "category",
                table: "categories",
                columns: new[] { "id", "is_default", "photo_id" },
                values: new object[] { new Guid("854cd0f7-4cea-4ad3-b68c-a3e84948cd3e"), false, new Guid("02f43b77-f3fc-4d97-a53c-a45fac195171") });

            migrationBuilder.InsertData(
                schema: "category",
                table: "category_translations",
                columns: new[] { "id", "category_id", "language_id", "name" },
                values: new object[,]
                {
                    { new Guid("3c2f5d1e-16f6-4109-b42f-ec4f1731ea62"), new Guid("854cd0f7-4cea-4ad3-b68c-a3e84948cd3e"), new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Danie" },
                    { new Guid("6729644a-7232-4483-bd90-7839732f6c7b"), new Guid("854cd0f7-4cea-4ad3-b68c-a3e84948cd3e"), new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Gericht" },
                    { new Guid("bfb53dfb-cb0f-4ba4-9ec6-4052b4e3ae98"), new Guid("854cd0f7-4cea-4ad3-b68c-a3e84948cd3e"), new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Dish" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("3c2f5d1e-16f6-4109-b42f-ec4f1731ea62"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("6729644a-7232-4483-bd90-7839732f6c7b"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("bfb53dfb-cb0f-4ba4-9ec6-4052b4e3ae98"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("854cd0f7-4cea-4ad3-b68c-a3e84948cd3e"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "photos",
                keyColumn: "id",
                keyValue: new Guid("02f43b77-f3fc-4d97-a53c-a45fac195171"));
        }
    }
}
