using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Language_And_Country_From_Profile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_profiles_countries_country_id1",
                schema: "user",
                table: "profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_profiles_currencies_currency_id1",
                schema: "user",
                table: "profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_profiles_languages_language_id1",
                schema: "user",
                table: "profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_users_profile_profile_temp_id",
                schema: "user",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_profiles_country_id",
                schema: "user",
                table: "profiles");

            migrationBuilder.DropIndex(
                name: "ix_profiles_language_id",
                schema: "user",
                table: "profiles");

            migrationBuilder.DropColumn(
                name: "country_id",
                schema: "user",
                table: "profiles");

            migrationBuilder.DropColumn(
                name: "language_id",
                schema: "user",
                table: "profiles");

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
                name: "fk_users_profiles_profile_temp_id",
                schema: "user",
                table: "users",
                column: "profile_id",
                principalSchema: "user",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_profiles_currencies_currency_id",
                schema: "user",
                table: "profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_users_profiles_profile_temp_id",
                schema: "user",
                table: "users");

            migrationBuilder.AddColumn<Guid>(
                name: "country_id",
                schema: "user",
                table: "profiles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "language_id",
                schema: "user",
                table: "profiles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_profiles_country_id",
                schema: "user",
                table: "profiles",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_profiles_language_id",
                schema: "user",
                table: "profiles",
                column: "language_id");

            migrationBuilder.AddForeignKey(
                name: "fk_profiles_countries_country_id1",
                schema: "user",
                table: "profiles",
                column: "country_id",
                principalSchema: "country",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_profiles_currencies_currency_id1",
                schema: "user",
                table: "profiles",
                column: "currency_id",
                principalSchema: "currency",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_profiles_languages_language_id1",
                schema: "user",
                table: "profiles",
                column: "language_id",
                principalSchema: "language",
                principalTable: "languages",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_users_profile_profile_temp_id",
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
