using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Default_Currency_Flag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_default",
                schema: "currency",
                table: "currencies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("3b56a6de-3b41-4b10-934f-469ca12f4fe3"),
                column: "is_default",
                value: false);

            migrationBuilder.UpdateData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("53dffab5-429d-4626-b1d9-f568119e069a"),
                column: "is_default",
                value: false);

            migrationBuilder.UpdateData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("8b0a0882-3eb5-495a-a646-06d7e0e9fe99"),
                column: "is_default",
                value: false);

            migrationBuilder.UpdateData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("e862f33f-a04a-4b4e-a4bb-9542b1db3eeb"),
                column: "is_default",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_default",
                schema: "currency",
                table: "currencies");
        }
    }
}
