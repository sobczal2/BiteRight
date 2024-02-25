using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Product_Rework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_amounts_amount_id",
                schema: "product",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "fk_products_users_user_id",
                schema: "product",
                table: "products");

            migrationBuilder.DropIndex(
                name: "ix_products_amount_id",
                schema: "product",
                table: "products");

            migrationBuilder.DropColumn(
                name: "amount_id",
                schema: "product",
                table: "products");

            migrationBuilder.DropColumn(
                name: "price_currency_id",
                schema: "product",
                table: "products");

            migrationBuilder.DropColumn(
                name: "price_value",
                schema: "product",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "product",
                table: "products",
                newName: "created_by_id");

            migrationBuilder.RenameIndex(
                name: "ix_products_user_id",
                schema: "product",
                table: "products",
                newName: "ix_products_created_by_id");

            migrationBuilder.AddColumn<Guid>(
                name: "product_id",
                schema: "product",
                table: "amounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "prices",
                schema: "product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<double>(type: "double precision", nullable: false),
                    currency_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prices", x => x.id);
                    table.ForeignKey(
                        name: "fk_prices_currencies_currency_id",
                        column: x => x.currency_id,
                        principalSchema: "currency",
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_prices_products_product_id",
                        column: x => x.product_id,
                        principalSchema: "product",
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_amounts_product_id",
                schema: "product",
                table: "amounts",
                column: "product_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_prices_currency_id",
                schema: "product",
                table: "prices",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_prices_product_id",
                schema: "product",
                table: "prices",
                column: "product_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_amounts_products_product_id",
                schema: "product",
                table: "amounts",
                column: "product_id",
                principalSchema: "product",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_products_users_created_by_id",
                schema: "product",
                table: "products",
                column: "created_by_id",
                principalSchema: "user",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_amounts_products_product_id",
                schema: "product",
                table: "amounts");

            migrationBuilder.DropForeignKey(
                name: "fk_products_users_created_by_id",
                schema: "product",
                table: "products");

            migrationBuilder.DropTable(
                name: "prices",
                schema: "product");

            migrationBuilder.DropIndex(
                name: "ix_amounts_product_id",
                schema: "product",
                table: "amounts");

            migrationBuilder.DropColumn(
                name: "product_id",
                schema: "product",
                table: "amounts");

            migrationBuilder.RenameColumn(
                name: "created_by_id",
                schema: "product",
                table: "products",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_products_created_by_id",
                schema: "product",
                table: "products",
                newName: "ix_products_user_id");

            migrationBuilder.AddColumn<Guid>(
                name: "amount_id",
                schema: "product",
                table: "products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "price_currency_id",
                schema: "product",
                table: "products",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "price_value",
                schema: "product",
                table: "products",
                type: "double precision",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_products_amount_id",
                schema: "product",
                table: "products",
                column: "amount_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_products_amounts_amount_id",
                schema: "product",
                table: "products",
                column: "amount_id",
                principalSchema: "product",
                principalTable: "amounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_products_users_user_id",
                schema: "product",
                table: "products",
                column: "user_id",
                principalSchema: "user",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
