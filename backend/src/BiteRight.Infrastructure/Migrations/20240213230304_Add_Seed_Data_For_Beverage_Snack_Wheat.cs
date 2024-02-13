using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Seed_Data_For_Beverage_Snack_Wheat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "category",
                table: "photos",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("4ceca44a-b13b-456a-9315-46b506076af4"), "snack.webp" },
                    { new Guid("a186163e-0551-4968-8706-543c470db6db"), "beverage.webp" },
                    { new Guid("a2f14ba8-b9be-40cd-9be0-b3c587be2fc3"), "wheat.webp" }
                });

            migrationBuilder.InsertData(
                schema: "category",
                table: "categories",
                columns: new[] { "id", "photo_id" },
                values: new object[,]
                {
                    { new Guid("7289cbc9-8249-4fc1-b2d3-bac90ad32595"), new Guid("a186163e-0551-4968-8706-543c470db6db") },
                    { new Guid("bf69966b-0cbc-4f5d-9388-c05926775cbf"), new Guid("a2f14ba8-b9be-40cd-9be0-b3c587be2fc3") },
                    { new Guid("e86caf03-ea3b-49ab-b499-68e387919fb6"), new Guid("4ceca44a-b13b-456a-9315-46b506076af4") }
                });

            migrationBuilder.InsertData(
                schema: "category",
                table: "category_translations",
                columns: new[] { "id", "category_id", "language_id", "name" },
                values: new object[,]
                {
                    { new Guid("24ba2bc6-8bc6-4a11-96dc-0de686de742f"), new Guid("bf69966b-0cbc-4f5d-9388-c05926775cbf"), new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Weizen" },
                    { new Guid("2e24ca76-c17c-4a80-874b-98a33b825567"), new Guid("7289cbc9-8249-4fc1-b2d3-bac90ad32595"), new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Beverage" },
                    { new Guid("329f0e4b-b364-41f9-ad75-804d796bcc74"), new Guid("7289cbc9-8249-4fc1-b2d3-bac90ad32595"), new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Napój" },
                    { new Guid("432866df-ef78-4eae-8cc3-0e2ab2efb801"), new Guid("bf69966b-0cbc-4f5d-9388-c05926775cbf"), new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Pszenica" },
                    { new Guid("4daa9144-5f43-401b-8622-3ac7b1cc14ff"), new Guid("e86caf03-ea3b-49ab-b499-68e387919fb6"), new Guid("24d48691-7325-4703-b69f-8db933a6736d"), "Przekąska" },
                    { new Guid("62334e0c-acba-47c7-a98a-9a606fed9084"), new Guid("7289cbc9-8249-4fc1-b2d3-bac90ad32595"), new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Getränk" },
                    { new Guid("718badd8-b2c2-4d4d-bd2b-2959f4949080"), new Guid("e86caf03-ea3b-49ab-b499-68e387919fb6"), new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Snack" },
                    { new Guid("b151fa8f-6361-40e5-99da-1ad19108af04"), new Guid("e86caf03-ea3b-49ab-b499-68e387919fb6"), new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"), "Snack" },
                    { new Guid("f121fb7f-ce98-4348-9e1d-9353c1d82df9"), new Guid("bf69966b-0cbc-4f5d-9388-c05926775cbf"), new Guid("454faf9a-644c-445c-89e3-b57203957c1a"), "Wheat" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("24ba2bc6-8bc6-4a11-96dc-0de686de742f"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("2e24ca76-c17c-4a80-874b-98a33b825567"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("329f0e4b-b364-41f9-ad75-804d796bcc74"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("432866df-ef78-4eae-8cc3-0e2ab2efb801"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("4daa9144-5f43-401b-8622-3ac7b1cc14ff"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("62334e0c-acba-47c7-a98a-9a606fed9084"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("718badd8-b2c2-4d4d-bd2b-2959f4949080"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("b151fa8f-6361-40e5-99da-1ad19108af04"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "category_translations",
                keyColumn: "id",
                keyValue: new Guid("f121fb7f-ce98-4348-9e1d-9353c1d82df9"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("7289cbc9-8249-4fc1-b2d3-bac90ad32595"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("bf69966b-0cbc-4f5d-9388-c05926775cbf"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("e86caf03-ea3b-49ab-b499-68e387919fb6"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "photos",
                keyColumn: "id",
                keyValue: new Guid("4ceca44a-b13b-456a-9315-46b506076af4"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "photos",
                keyColumn: "id",
                keyValue: new Guid("a186163e-0551-4968-8706-543c470db6db"));

            migrationBuilder.DeleteData(
                schema: "category",
                table: "photos",
                keyColumn: "id",
                keyValue: new Guid("a2f14ba8-b9be-40cd-9be0-b3c587be2fc3"));
        }
    }
}
