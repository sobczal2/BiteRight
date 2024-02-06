using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "category");

            migrationBuilder.EnsureSchema(
                name: "country");

            migrationBuilder.EnsureSchema(
                name: "currency");

            migrationBuilder.EnsureSchema(
                name: "language");

            migrationBuilder.EnsureSchema(
                name: "product");

            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.CreateTable(
                name: "currencies",
                schema: "currency",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    symbol = table.Column<string>(type: "text", nullable: false),
                    iso4217code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currencies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "languages",
                schema: "language",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    native_name = table.Column<string>(type: "text", nullable: false),
                    english_name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_languages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "photos",
                schema: "category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_photos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "profiles",
                schema: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    currency_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_profiles", x => x.id);
                    table.ForeignKey(
                        name: "fk_profiles_currencies_currency_temp_id2",
                        column: x => x.currency_id,
                        principalSchema: "currency",
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                schema: "country",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    native_name = table.Column<string>(type: "text", nullable: false),
                    english_name = table.Column<string>(type: "text", nullable: false),
                    alpha2code = table.Column<string>(type: "text", nullable: false),
                    official_language_id = table.Column<Guid>(type: "uuid", nullable: false),
                    currency_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countries", x => x.id);
                    table.ForeignKey(
                        name: "fk_countries_currencies_currency_temp_id",
                        column: x => x.currency_id,
                        principalSchema: "currency",
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_countries_languages_official_language_temp_id1",
                        column: x => x.official_language_id,
                        principalSchema: "language",
                        principalTable: "languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    photo_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_categories_photo_photo_temp_id",
                        column: x => x.photo_id,
                        principalSchema: "category",
                        principalTable: "photos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    identity_id = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    joined_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    profile_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_profiles_profile_temp_id",
                        column: x => x.profile_id,
                        principalSchema: "user",
                        principalTable: "profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "category_translations",
                schema: "category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    language_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_translations", x => x.id);
                    table.ForeignKey(
                        name: "fk_category_translations_categories_category_temp_id1",
                        column: x => x.category_id,
                        principalSchema: "category",
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_category_translations_languages_language_temp_id",
                        column: x => x.language_id,
                        principalSchema: "language",
                        principalTable: "languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                schema: "product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price_value = table.Column<decimal>(type: "numeric", nullable: true),
                    price_currency_id = table.Column<Guid>(type: "uuid", nullable: true),
                    expiration_date_value = table.Column<DateOnly>(type: "date", nullable: false),
                    expiration_date_kind = table.Column<int>(type: "integer", nullable: false),
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

            migrationBuilder.InsertData(
                schema: "currency",
                table: "currencies",
                columns: new[] { "id", "iso4217code", "name", "symbol" },
                values: new object[,]
                {
                    { new Guid("3b56a6de-3b41-4b10-934f-469ca12f4fe3"), "PLN", "Polski złoty", "zł" },
                    { new Guid("53dffab5-429d-4626-b1d9-f568119e069a"), "GBP", "Pound sterling", "£" },
                    { new Guid("8b0a0882-3eb5-495a-a646-06d7e0e9fe99"), "EUR", "Euro", "€" },
                    { new Guid("e862f33f-a04a-4b4e-a4bb-9542b1db3eeb"), "USD", "United States dollar", "$" }
                });

            migrationBuilder.InsertData(
                schema: "language",
                table: "languages",
                columns: new[] { "id", "code", "english_name", "native_name" },
                values: new object[,]
                {
                    { new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "pl", "Polish", "Polski" },
                    { new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "en", "English", "English" },
                    { new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "de", "German", "Deutsch" }
                });

            migrationBuilder.InsertData(
                schema: "category",
                table: "photos",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("2bfd1c0c-8882-44fa-b73d-8588ad8ec50b"), "fish.webp" },
                    { new Guid("2eaee2ac-3ebf-49f2-807b-1b0509f528ba"), "vegetable.webp" },
                    { new Guid("4d4c96bc-6990-4b94-982e-d5e7860019a1"), "meat.webp" },
                    { new Guid("5e4d81da-841b-493a-a47b-9f69791e1063"), "fruit.webp" },
                    { new Guid("98eb4dc2-11b5-440b-bfc1-742fda8279b7"), "dairy.webp" }
                });

            migrationBuilder.InsertData(
                schema: "category",
                table: "categories",
                columns: new[] { "id", "photo_id" },
                values: new object[,]
                {
                    { new Guid("17c56168-c9ec-4ffb-a074-495a02ab0359"), new Guid("2bfd1c0c-8882-44fa-b73d-8588ad8ec50b") },
                    { new Guid("1fd7ed59-9e34-40ab-a03d-6282b5d9fd86"), new Guid("5e4d81da-841b-493a-a47b-9f69791e1063") },
                    { new Guid("349774c7-3249-4245-a1e2-5b70c5725bbf"), new Guid("2eaee2ac-3ebf-49f2-807b-1b0509f528ba") },
                    { new Guid("5e40ba93-d28c-4cf3-9e75-379040a18e52"), new Guid("4d4c96bc-6990-4b94-982e-d5e7860019a1") },
                    { new Guid("e8c78317-70ac-4051-805e-ece2bb37656f"), new Guid("98eb4dc2-11b5-440b-bfc1-742fda8279b7") }
                });

            migrationBuilder.InsertData(
                schema: "country",
                table: "countries",
                columns: new[] { "id", "alpha2code", "currency_id", "english_name", "native_name", "official_language_id" },
                values: new object[,]
                {
                    { new Guid("12e2937f-f04d-4150-a7ae-5ab1176a95d8"), "US", new Guid("e862f33f-a04a-4b4e-a4bb-9542b1db3eeb"), "United States of America", "United States of America", new Guid("454faf9a-644c-445c-89e3-b57203957c1a") },
                    { new Guid("1352de6e-c0bf-48c6-b703-fae0b254d642"), "DE", new Guid("8b0a0882-3eb5-495a-a646-06d7e0e9fe99"), "Germany", "Deutschland", new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3") },
                    { new Guid("35d08361-f753-4db9-b88e-11c400d53eb7"), "PL", new Guid("3b56a6de-3b41-4b10-934f-469ca12f4fe3"), "Poland", "Polska", new Guid("24d48691-7325-4703-b69f-8db933a6736d") },
                    { new Guid("f3e4c5cb-229c-4b2d-90dc-f83cb4a45f75"), "EN", new Guid("53dffab5-429d-4626-b1d9-f568119e069a"), "England", "England", new Guid("454faf9a-644c-445c-89e3-b57203957c1a") }
                });

            migrationBuilder.InsertData(
                schema: "category",
                table: "category_translations",
                columns: new[] { "id", "category_id", "language_id", "name" },
                values: new object[,]
                {
                    { new Guid("04b42267-4c3e-4ac1-9972-90112ba7c952"), new Guid("5e40ba93-d28c-4cf3-9e75-379040a18e52"), new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Fleisch" },
                    { new Guid("115fbaa7-0bcc-4d41-b1f1-43fe1cdb28f9"), new Guid("349774c7-3249-4245-a1e2-5b70c5725bbf"), new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Gemüse" },
                    { new Guid("15e9f981-3a22-4bb2-96f7-5cc559567794"), new Guid("17c56168-c9ec-4ffb-a074-495a02ab0359"), new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Ryby" },
                    { new Guid("38097234-329c-4372-a54f-13e6c41004fa"), new Guid("e8c78317-70ac-4051-805e-ece2bb37656f"), new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Dairy" },
                    { new Guid("532bfdc2-bd1a-40eb-8723-65ae4e76f242"), new Guid("1fd7ed59-9e34-40ab-a03d-6282b5d9fd86"), new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Obst" },
                    { new Guid("73127f14-9fe8-4dd7-b4bf-e11d98c6e505"), new Guid("1fd7ed59-9e34-40ab-a03d-6282b5d9fd86"), new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Fruit" },
                    { new Guid("7ec1928d-1517-4ef1-a24f-cf20322f34a5"), new Guid("349774c7-3249-4245-a1e2-5b70c5725bbf"), new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Vegetable" },
                    { new Guid("96a2257a-f4c4-4c47-9b5b-50a7be92c5da"), new Guid("e8c78317-70ac-4051-805e-ece2bb37656f"), new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Nabiał" },
                    { new Guid("a2c2d7cb-e684-4c71-80e0-07aa1db6d5ba"), new Guid("5e40ba93-d28c-4cf3-9e75-379040a18e52"), new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Meat" },
                    { new Guid("af5072c0-7282-481f-a623-f4e00feb2bf8"), new Guid("1fd7ed59-9e34-40ab-a03d-6282b5d9fd86"), new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Owoce" },
                    { new Guid("b9e41376-b606-42d3-b2af-8373c1905b87"), new Guid("349774c7-3249-4245-a1e2-5b70c5725bbf"), new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Warzywa" },
                    { new Guid("bd8fefc6-70ce-4562-9730-274c589ca72b"), new Guid("17c56168-c9ec-4ffb-a074-495a02ab0359"), new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Fish" },
                    { new Guid("c089f04f-fdfe-48e1-87b7-ff8028ecb67f"), new Guid("e8c78317-70ac-4051-805e-ece2bb37656f"), new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Milchprodukte" },
                    { new Guid("c605cdf4-8b17-4cf0-950a-5a9bee434145"), new Guid("17c56168-c9ec-4ffb-a074-495a02ab0359"), new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Fisch" },
                    { new Guid("f7f1c6e7-012f-449c-95a3-79459f9331fb"), new Guid("5e40ba93-d28c-4cf3-9e75-379040a18e52"), new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Mięso" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_categories_photo_id",
                schema: "category",
                table: "categories",
                column: "photo_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_category_translations_category_id",
                schema: "category",
                table: "category_translations",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_category_translations_language_id",
                schema: "category",
                table: "category_translations",
                column: "language_id");

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

            migrationBuilder.CreateIndex(
                name: "ix_profiles_currency_id",
                schema: "user",
                table: "profiles",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_identity_id",
                schema: "user",
                table: "users",
                column: "identity_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_profile_id",
                schema: "user",
                table: "users",
                column: "profile_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "category_translations",
                schema: "category");

            migrationBuilder.DropTable(
                name: "countries",
                schema: "country");

            migrationBuilder.DropTable(
                name: "products",
                schema: "product");

            migrationBuilder.DropTable(
                name: "languages",
                schema: "language");

            migrationBuilder.DropTable(
                name: "categories",
                schema: "category");

            migrationBuilder.DropTable(
                name: "users",
                schema: "user");

            migrationBuilder.DropTable(
                name: "photos",
                schema: "category");

            migrationBuilder.DropTable(
                name: "profiles",
                schema: "user");

            migrationBuilder.DropTable(
                name: "currencies",
                schema: "currency");
        }
    }
}
