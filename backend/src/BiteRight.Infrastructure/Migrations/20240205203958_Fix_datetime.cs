using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix_datetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price_Value",
                schema: "product",
                table: "products",
                newName: "price_value");

            migrationBuilder.RenameColumn(
                name: "expiration_date_Value",
                schema: "product",
                table: "products",
                newName: "expiration_date_value");

            migrationBuilder.RenameColumn(
                name: "expiration_date_Kind",
                schema: "product",
                table: "products",
                newName: "expiration_date_kind");

            migrationBuilder.RenameColumn(
                name: "price_CurrencyId",
                schema: "product",
                table: "products",
                newName: "price_currency_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "joined_at",
                schema: "user",
                table: "users",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "added_date_time",
                schema: "product",
                table: "products",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price_value",
                schema: "product",
                table: "products",
                newName: "price_Value");

            migrationBuilder.RenameColumn(
                name: "expiration_date_value",
                schema: "product",
                table: "products",
                newName: "expiration_date_Value");

            migrationBuilder.RenameColumn(
                name: "expiration_date_kind",
                schema: "product",
                table: "products",
                newName: "expiration_date_Kind");

            migrationBuilder.RenameColumn(
                name: "price_currency_id",
                schema: "product",
                table: "products",
                newName: "price_CurrencyId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "joined_at",
                schema: "user",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "added_date_time",
                schema: "product",
                table: "products",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");
        }
    }
}
