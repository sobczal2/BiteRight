using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class photo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "photo",
                schema: "category",
                table: "categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("17c56168-c9ec-4ffb-a074-495a02ab0359"),
                column: "photo",
                value: null);

            migrationBuilder.UpdateData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("1fd7ed59-9e34-40ab-a03d-6282b5d9fd86"),
                column: "photo",
                value: null);

            migrationBuilder.UpdateData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("349774c7-3249-4245-a1e2-5b70c5725bbf"),
                column: "photo",
                value: null);

            migrationBuilder.UpdateData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("5e40ba93-d28c-4cf3-9e75-379040a18e52"),
                column: "photo",
                value: null);

            migrationBuilder.UpdateData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("e8c78317-70ac-4051-805e-ece2bb37656f"),
                column: "photo",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo",
                schema: "category",
                table: "categories");
        }
    }
}
