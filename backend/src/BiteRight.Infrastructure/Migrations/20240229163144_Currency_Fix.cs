using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Currency_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("e862f33f-a04a-4b4e-a4bb-9542b1db3eeb"),
                column: "is_default",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "currency",
                table: "currencies",
                keyColumn: "id",
                keyValue: new Guid("e862f33f-a04a-4b4e-a4bb-9542b1db3eeb"),
                column: "is_default",
                value: false);
        }
    }
}
