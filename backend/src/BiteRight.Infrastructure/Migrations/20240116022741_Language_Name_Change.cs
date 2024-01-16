using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Language_Name_Change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_profiles_countries_country_id",
                schema: "user",
                table: "profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_profiles_currencies_currency_id",
                schema: "user",
                table: "profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_profiles_languages_language_id",
                schema: "user",
                table: "profiles");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "language",
                table: "languages",
                newName: "native_name");

            migrationBuilder.AddColumn<string>(
                name: "english_name",
                schema: "language",
                table: "languages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "language",
                table: "languages",
                keyColumn: "id",
                keyValue: new Guid("24d48691-7325-4703-b69f-8db933a6736d"),
                column: "english_name",
                value: "Polish");

            migrationBuilder.UpdateData(
                schema: "language",
                table: "languages",
                keyColumn: "id",
                keyValue: new Guid("454faf9a-644c-445c-89e3-b57203957c1a"),
                column: "english_name",
                value: "English");

            migrationBuilder.UpdateData(
                schema: "language",
                table: "languages",
                keyColumn: "id",
                keyValue: new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"),
                column: "english_name",
                value: "German");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "english_name",
                schema: "language",
                table: "languages");

            migrationBuilder.RenameColumn(
                name: "native_name",
                schema: "language",
                table: "languages",
                newName: "name");

            migrationBuilder.AddForeignKey(
                name: "fk_profiles_countries_country_id",
                schema: "user",
                table: "profiles",
                column: "country_id",
                principalSchema: "country",
                principalTable: "countries",
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
                name: "fk_profiles_languages_language_id",
                schema: "user",
                table: "profiles",
                column: "language_id",
                principalSchema: "language",
                principalTable: "languages",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
