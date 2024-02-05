using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fixes_In_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_profiles_currencies_currency_id",
                schema: "user",
                table: "profiles");

            migrationBuilder.EnsureSchema(
                name: "product");

            migrationBuilder.CreateTable(
                name: "products",
                schema: "product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price_Value = table.Column<decimal>(type: "numeric", nullable: true),
                    price_CurrencyId = table.Column<Guid>(type: "uuid", nullable: true),
                    expiration_date_Value = table.Column<DateOnly>(type: "date", nullable: false),
                    expiration_date_Kind = table.Column<int>(type: "integer", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    added_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    usage = table.Column<double>(type: "double precision", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_products_categories_category_id",
                        column: x => x.category_id,
                        principalSchema: "category",
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_products_users_user_temp_id1",
                        column: x => x.user_id,
                        principalSchema: "user",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_countries_currency_id",
                schema: "country",
                table: "countries",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_countries_official_language_id",
                schema: "country",
                table: "countries",
                column: "official_language_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_category_id",
                schema: "product",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_user_id",
                schema: "product",
                table: "products",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_countries_currencies_currency_temp_id",
                schema: "country",
                table: "countries",
                column: "currency_id",
                principalSchema: "currency",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_countries_languages_official_language_temp_id1",
                schema: "country",
                table: "countries",
                column: "official_language_id",
                principalSchema: "language",
                principalTable: "languages",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_profiles_currencies_currency_temp_id2",
                schema: "user",
                table: "profiles",
                column: "currency_id",
                principalSchema: "currency",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_countries_currencies_currency_temp_id",
                schema: "country",
                table: "countries");

            migrationBuilder.DropForeignKey(
                name: "fk_countries_languages_official_language_temp_id1",
                schema: "country",
                table: "countries");

            migrationBuilder.DropForeignKey(
                name: "fk_profiles_currencies_currency_temp_id2",
                schema: "user",
                table: "profiles");

            migrationBuilder.DropTable(
                name: "products",
                schema: "product");

            migrationBuilder.DropIndex(
                name: "ix_countries_currency_id",
                schema: "country",
                table: "countries");

            migrationBuilder.DropIndex(
                name: "ix_countries_official_language_id",
                schema: "country",
                table: "countries");

            migrationBuilder.AddForeignKey(
                name: "fk_profiles_currencies_currency_id",
                schema: "user",
                table: "profiles",
                column: "currency_id",
                principalSchema: "currency",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
