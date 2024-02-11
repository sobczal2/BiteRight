using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Refactor_Product_Amount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_categories_photo_photo_temp_id",
                schema: "category",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "fk_products_categories_category_id",
                schema: "product",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "fk_products_users_user_temp_id1",
                schema: "product",
                table: "products");

            migrationBuilder.DropColumn(
                name: "consumption",
                schema: "product",
                table: "products");

            migrationBuilder.EnsureSchema(
                name: "unit");

            migrationBuilder.AddColumn<Guid>(
                name: "amount_id",
                schema: "product",
                table: "products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "units",
                schema: "unit",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    unit_system = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_units", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "amounts",
                schema: "product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    current_value = table.Column<double>(type: "double precision", nullable: false),
                    max_value = table.Column<double>(type: "double precision", nullable: false),
                    unit_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_amounts", x => x.id);
                    table.ForeignKey(
                        name: "fk_amounts_units_unit_temp_id2",
                        column: x => x.unit_id,
                        principalSchema: "unit",
                        principalTable: "units",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "unit_translations",
                schema: "unit",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    unit_id = table.Column<Guid>(type: "uuid", nullable: false),
                    language_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    short_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unit_translations", x => x.id);
                    table.ForeignKey(
                        name: "fk_unit_translations_languages_language_temp_id3",
                        column: x => x.language_id,
                        principalSchema: "language",
                        principalTable: "languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_unit_translations_units_unit_temp_id1",
                        column: x => x.unit_id,
                        principalSchema: "unit",
                        principalTable: "units",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "unit",
                table: "units",
                columns: new[] { "id", "unit_system" },
                values: new object[,]
                {
                    { new Guid("b6d4d4dd-c035-4047-b8ee-48937cb1f368"), 1 },
                    { new Guid("cde52e6c-5d9d-4876-978a-bf67c02cc8be"), 1 }
                });

            migrationBuilder.InsertData(
                schema: "unit",
                table: "unit_translations",
                columns: new[] { "id", "language_id", "name", "short_name", "unit_id" },
                values: new object[,]
                {
                    { new Guid("1622a17d-e814-4479-a36f-fd4f150baf3d"), new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Kilogramm", "kg", new Guid("cde52e6c-5d9d-4876-978a-bf67c02cc8be") },
                    { new Guid("3ca1440b-7bb2-4cfc-99a2-480eaf9f80ce"), new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Litr", "L", new Guid("b6d4d4dd-c035-4047-b8ee-48937cb1f368") },
                    { new Guid("4aa58fa5-983d-4421-8c31-b277eea738ff"), new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Liter", "L", new Guid("b6d4d4dd-c035-4047-b8ee-48937cb1f368") },
                    { new Guid("62e0417f-c78c-4e4d-86cc-440e22a6694b"), new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Kilogram", "kg", new Guid("cde52e6c-5d9d-4876-978a-bf67c02cc8be") },
                    { new Guid("719f0522-a85a-49cf-82d9-30d76b804b1d"), new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Kilogram", "kg", new Guid("cde52e6c-5d9d-4876-978a-bf67c02cc8be") },
                    { new Guid("e9dafc35-4599-40bf-ae44-09432e7b3379"), new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Liter", "L", new Guid("b6d4d4dd-c035-4047-b8ee-48937cb1f368") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_products_amount_id",
                schema: "product",
                table: "products",
                column: "amount_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_amounts_unit_id",
                schema: "product",
                table: "amounts",
                column: "unit_id");

            migrationBuilder.CreateIndex(
                name: "ix_unit_translations_language_id",
                schema: "unit",
                table: "unit_translations",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "ix_unit_translations_unit_id",
                schema: "unit",
                table: "unit_translations",
                column: "unit_id");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_categories_photos_photo_temp_id",
                schema: "category",
                table: "categories");

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

            migrationBuilder.DropTable(
                name: "amounts",
                schema: "product");

            migrationBuilder.DropTable(
                name: "unit_translations",
                schema: "unit");

            migrationBuilder.DropTable(
                name: "units",
                schema: "unit");

            migrationBuilder.DropIndex(
                name: "ix_products_amount_id",
                schema: "product",
                table: "products");

            migrationBuilder.DropColumn(
                name: "amount_id",
                schema: "product",
                table: "products");

            migrationBuilder.AddColumn<double>(
                name: "consumption",
                schema: "product",
                table: "products",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "fk_categories_photo_photo_temp_id",
                schema: "category",
                table: "categories",
                column: "photo_id",
                principalSchema: "category",
                principalTable: "photos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

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
                name: "fk_products_users_user_temp_id1",
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
