using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Category_None_To_Other : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("206a3c95-fb6d-4127-a37b-9f328c021021"),
                column: "name",
                value: "Andere");

            migrationBuilder.UpdateData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("abed62c9-41b4-462f-866b-06d714dec958"),
                column: "name",
                value: "Inne");

            migrationBuilder.UpdateData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("f7b5e10f-0719-4731-831a-ffe0a1a1ed07"),
                column: "name",
                value: "Other");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("206a3c95-fb6d-4127-a37b-9f328c021021"),
                column: "name",
                value: "Keine");

            migrationBuilder.UpdateData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("abed62c9-41b4-462f-866b-06d714dec958"),
                column: "name",
                value: "Brak");

            migrationBuilder.UpdateData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("f7b5e10f-0719-4731-831a-ffe0a1a1ed07"),
                column: "name",
                value: "None");
        }
    }
}
