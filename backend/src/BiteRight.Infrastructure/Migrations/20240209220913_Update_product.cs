using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "disposed",
                schema: "product",
                table: "products",
                newName: "disposed_state_disposed");

            migrationBuilder.AddColumn<DateTime>(
                name: "disposed_state_disposed_date",
                schema: "product",
                table: "products",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "disposed_state_disposed_date",
                schema: "product",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "disposed_state_disposed",
                schema: "product",
                table: "products",
                newName: "disposed");
        }
    }
}
