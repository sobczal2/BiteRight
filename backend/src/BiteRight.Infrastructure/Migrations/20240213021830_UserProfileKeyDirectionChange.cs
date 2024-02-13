using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserProfileKeyDirectionChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_amounts_units_unit_temp_id2",
                schema: "product",
                table: "amounts");

            migrationBuilder.DropForeignKey(
                name: "fk_categories_photos_photo_temp_id",
                schema: "category",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "fk_category_translations_categories_category_temp_id1",
                schema: "category",
                table: "category_translations");

            migrationBuilder.DropForeignKey(
                name: "fk_category_translations_languages_language_temp_id",
                schema: "category",
                table: "category_translations");

            migrationBuilder.DropForeignKey(
                name: "fk_countries_currencies_currency_temp_id",
                schema: "country",
                table: "countries");

            migrationBuilder.DropForeignKey(
                name: "fk_countries_languages_official_language_temp_id1",
                schema: "country",
                table: "countries");

            migrationBuilder.DropForeignKey(
                name: "fk_products_amounts_amount_temp_id",
                schema: "product",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "fk_products_categories_category_temp_id2",
                schema: "product",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "fk_products_users_user_temp_id",
                schema: "product",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "fk_profiles_currencies_currency_temp_id2",
                schema: "user",
                table: "profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_unit_translations_languages_language_temp_id3",
                schema: "unit",
                table: "unit_translations");

            migrationBuilder.DropForeignKey(
                name: "fk_unit_translations_units_unit_temp_id1",
                schema: "unit",
                table: "unit_translations");

            migrationBuilder.DropForeignKey(
                name: "fk_users_profiles_profile_id",
                schema: "user",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_profile_id",
                schema: "user",
                table: "users");

            migrationBuilder.DropColumn(
                name: "profile_id",
                schema: "user",
                table: "users");

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                schema: "user",
                table: "profiles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_profiles_user_id",
                schema: "user",
                table: "profiles",
                column: "user_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_amounts_units_unit_id",
                schema: "product",
                table: "amounts",
                column: "unit_id",
                principalSchema: "unit",
                principalTable: "units",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_categories_photos_photo_id",
                schema: "category",
                table: "categories",
                column: "photo_id",
                principalSchema: "category",
                principalTable: "photos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_category_translations_categories_category_id",
                schema: "category",
                table: "category_translations",
                column: "category_id",
                principalSchema: "category",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_category_translations_languages_language_id",
                schema: "category",
                table: "category_translations",
                column: "language_id",
                principalSchema: "language",
                principalTable: "languages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_countries_currencies_currency_id",
                schema: "country",
                table: "countries",
                column: "currency_id",
                principalSchema: "currency",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_countries_languages_official_language_id",
                schema: "country",
                table: "countries",
                column: "official_language_id",
                principalSchema: "language",
                principalTable: "languages",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

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
                name: "fk_products_categories_category_id",
                schema: "product",
                table: "products",
                column: "category_id",
                principalSchema: "category",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_products_users_user_id",
                schema: "product",
                table: "products",
                column: "user_id",
                principalSchema: "user",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_profiles_currencies_currency_id",
                schema: "user",
                table: "profiles",
                column: "currency_id",
                principalSchema: "currency",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_profiles_users_user_id",
                schema: "user",
                table: "profiles",
                column: "user_id",
                principalSchema: "user",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_unit_translations_languages_language_id",
                schema: "unit",
                table: "unit_translations",
                column: "language_id",
                principalSchema: "language",
                principalTable: "languages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_unit_translations_units_unit_id",
                schema: "unit",
                table: "unit_translations",
                column: "unit_id",
                principalSchema: "unit",
                principalTable: "units",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_amounts_units_unit_id",
                schema: "product",
                table: "amounts");

            migrationBuilder.DropForeignKey(
                name: "fk_categories_photos_photo_id",
                schema: "category",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "fk_category_translations_categories_category_id",
                schema: "category",
                table: "category_translations");

            migrationBuilder.DropForeignKey(
                name: "fk_category_translations_languages_language_id",
                schema: "category",
                table: "category_translations");

            migrationBuilder.DropForeignKey(
                name: "fk_countries_currencies_currency_id",
                schema: "country",
                table: "countries");

            migrationBuilder.DropForeignKey(
                name: "fk_countries_languages_official_language_id",
                schema: "country",
                table: "countries");

            migrationBuilder.DropForeignKey(
                name: "fk_products_amounts_amount_id",
                schema: "product",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "fk_products_categories_category_id",
                schema: "product",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "fk_products_users_user_id",
                schema: "product",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "fk_profiles_currencies_currency_id",
                schema: "user",
                table: "profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_profiles_users_user_id",
                schema: "user",
                table: "profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_unit_translations_languages_language_id",
                schema: "unit",
                table: "unit_translations");

            migrationBuilder.DropForeignKey(
                name: "fk_unit_translations_units_unit_id",
                schema: "unit",
                table: "unit_translations");

            migrationBuilder.DropIndex(
                name: "ix_profiles_user_id",
                schema: "user",
                table: "profiles");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "user",
                table: "profiles");

            migrationBuilder.AddColumn<Guid>(
                name: "profile_id",
                schema: "user",
                table: "users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_users_profile_id",
                schema: "user",
                table: "users",
                column: "profile_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_amounts_units_unit_temp_id2",
                schema: "product",
                table: "amounts",
                column: "unit_id",
                principalSchema: "unit",
                principalTable: "units",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_categories_photos_photo_temp_id",
                schema: "category",
                table: "categories",
                column: "photo_id",
                principalSchema: "category",
                principalTable: "photos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_category_translations_categories_category_temp_id1",
                schema: "category",
                table: "category_translations",
                column: "category_id",
                principalSchema: "category",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_category_translations_languages_language_temp_id",
                schema: "category",
                table: "category_translations",
                column: "language_id",
                principalSchema: "language",
                principalTable: "languages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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
                name: "fk_products_amounts_amount_temp_id",
                schema: "product",
                table: "products",
                column: "amount_id",
                principalSchema: "product",
                principalTable: "amounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_products_categories_category_temp_id2",
                schema: "product",
                table: "products",
                column: "category_id",
                principalSchema: "category",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_products_users_user_temp_id",
                schema: "product",
                table: "products",
                column: "user_id",
                principalSchema: "user",
                principalTable: "users",
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

            migrationBuilder.AddForeignKey(
                name: "fk_unit_translations_languages_language_temp_id3",
                schema: "unit",
                table: "unit_translations",
                column: "language_id",
                principalSchema: "language",
                principalTable: "languages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_unit_translations_units_unit_temp_id1",
                schema: "unit",
                table: "unit_translations",
                column: "unit_id",
                principalSchema: "unit",
                principalTable: "units",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_users_profiles_profile_id",
                schema: "user",
                table: "users",
                column: "profile_id",
                principalSchema: "user",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
