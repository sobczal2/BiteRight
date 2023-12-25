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
                name: "language");

            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
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
                    official_language_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "languages",
                schema: "language",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_languages", x => x.id);
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
                    joined_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
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
                        name: "fk_category_translations_categories_category_id",
                        column: x => x.category_id,
                        principalSchema: "category",
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_category_translations_languages_language_temp_id1",
                        column: x => x.language_id,
                        principalSchema: "language",
                        principalTable: "languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "category",
                table: "categories",
                column: "id",
                values: new object[]
                {
                    new Guid("17c56168-c9ec-4ffb-a074-495a02ab0359"),
                    new Guid("1fd7ed59-9e34-40ab-a03d-6282b5d9fd86"),
                    new Guid("349774c7-3249-4245-a1e2-5b70c5725bbf"),
                    new Guid("5e40ba93-d28c-4cf3-9e75-379040a18e52"),
                    new Guid("e8c78317-70ac-4051-805e-ece2bb37656f")
                });

            migrationBuilder.InsertData(
                schema: "country",
                table: "countries",
                columns: new[] { "id", "alpha2code", "english_name", "native_name", "official_language_id" },
                values: new object[,]
                {
                    { new Guid("12e2937f-f04d-4150-a7ae-5ab1176a95d8"), "us", "United States of America", "United States of America", new Guid("454faf9a-644c-445c-89e3-b57203957c1a") },
                    { new Guid("1352de6e-c0bf-48c6-b703-fae0b254d642"), "de", "Germany", "Deutschland", new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3") },
                    { new Guid("35d08361-f753-4db9-b88e-11c400d53eb7"), "pl", "Poland", "Polska", new Guid("24d48691-7325-4703-b69f-8db933a6736d") },
                    { new Guid("f3e4c5cb-229c-4b2d-90dc-f83cb4a45f75"), "en", "England", "England", new Guid("454faf9a-644c-445c-89e3-b57203957c1a") }
                });

            migrationBuilder.InsertData(
                schema: "language",
                table: "languages",
                columns: new[] { "id", "code", "name" },
                values: new object[,]
                {
                    { new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "pl", "Polski" },
                    { new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "en", "English" },
                    { new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "de", "Deutsch" }
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
                name: "ix_users_identity_id",
                schema: "user",
                table: "users",
                column: "identity_id",
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
                name: "users",
                schema: "user");

            migrationBuilder.DropTable(
                name: "categories",
                schema: "category");

            migrationBuilder.DropTable(
                name: "languages",
                schema: "language");
        }
    }
}
