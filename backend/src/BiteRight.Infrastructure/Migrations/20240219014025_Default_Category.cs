using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Default_Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_default",
                schema: "category",
                table: "categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("17c56168-c9ec-4ffb-a074-495a02ab0359"),
                column: "is_default",
                value: false);

            migrationBuilder.UpdateData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("1fd7ed59-9e34-40ab-a03d-6282b5d9fd86"),
                column: "is_default",
                value: false);

            migrationBuilder.UpdateData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("349774c7-3249-4245-a1e2-5b70c5725bbf"),
                column: "is_default",
                value: false);

            migrationBuilder.UpdateData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("5e40ba93-d28c-4cf3-9e75-379040a18e52"),
                column: "is_default",
                value: false);

            migrationBuilder.UpdateData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("7289cbc9-8249-4fc1-b2d3-bac90ad32595"),
                column: "is_default",
                value: false);

            migrationBuilder.UpdateData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("bf69966b-0cbc-4f5d-9388-c05926775cbf"),
                column: "is_default",
                value: false);

            migrationBuilder.UpdateData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("c82e0550-26cf-410d-8cec-5cf62bada757"),
                column: "is_default",
                value: true);

            migrationBuilder.UpdateData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("e86caf03-ea3b-49ab-b499-68e387919fb6"),
                column: "is_default",
                value: false);

            migrationBuilder.UpdateData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("e8c78317-70ac-4051-805e-ece2bb37656f"),
                column: "is_default",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_default",
                schema: "category",
                table: "categories");
        }
    }
}
