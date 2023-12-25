using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Language_And_Country : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "currencies",
                schema: "currency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                schema: "user",
                table: "users");

            migrationBuilder.DropColumn(
                name: "IsEmailVerified",
                schema: "user",
                table: "users");

            migrationBuilder.EnsureSchema(
                name: "country");

            migrationBuilder.EnsureSchema(
                name: "language");

            migrationBuilder.RenameColumn(
                name: "Username",
                schema: "user",
                table: "users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "user",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "user",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "JoinedAt",
                schema: "user",
                table: "users",
                newName: "joined_at");

            migrationBuilder.RenameColumn(
                name: "IdentityId",
                schema: "user",
                table: "users",
                newName: "identity_id");

            migrationBuilder.RenameIndex(
                name: "IX_users_IdentityId",
                schema: "user",
                table: "users",
                newName: "ix_users_identity_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                schema: "user",
                table: "users",
                column: "id");

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

            migrationBuilder.InsertData(
                schema: "country",
                table: "countries",
                columns: new[] { "id", "alpha2code", "english_name", "native_name", "official_language_id" },
                values: new object[,]
                {
                    { new Guid("9359cc21-dc4a-45ab-84f4-dc049868ef1a"), "en", "England", "England", new Guid("cd590129-82a0-4e30-a6ea-6f140db30dbc") },
                    { new Guid("ad6d2f06-07d0-4b72-8f7e-2ed4f4fbdb96"), "us", "United States of America", "United States of America", new Guid("cd590129-82a0-4e30-a6ea-6f140db30dbc") },
                    { new Guid("ebd7857a-1f4b-4a2c-9de0-d7abec20025b"), "de", "Germany", "Deutschland", new Guid("5573f2d0-01bc-4328-8dfd-524a622cc06d") },
                    { new Guid("fcf39857-7d3a-47cd-9860-4904d29017ef"), "pl", "Poland", "Polska", new Guid("d0cc3ba0-b282-4e37-ae5e-06103000a2ce") }
                });

            migrationBuilder.InsertData(
                schema: "language",
                table: "languages",
                columns: new[] { "id", "code", "name" },
                values: new object[,]
                {
                    { new Guid("5573f2d0-01bc-4328-8dfd-524a622cc06d"), "de", "Deutsch" },
                    { new Guid("cd590129-82a0-4e30-a6ea-6f140db30dbc"), "en", "English" },
                    { new Guid("d0cc3ba0-b282-4e37-ae5e-06103000a2ce"), "pl", "Polski" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "countries",
                schema: "country");

            migrationBuilder.DropTable(
                name: "languages",
                schema: "language");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                schema: "user",
                table: "users");

            migrationBuilder.EnsureSchema(
                name: "currency");

            migrationBuilder.RenameColumn(
                name: "username",
                schema: "user",
                table: "users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "email",
                schema: "user",
                table: "users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "user",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "joined_at",
                schema: "user",
                table: "users",
                newName: "JoinedAt");

            migrationBuilder.RenameColumn(
                name: "identity_id",
                schema: "user",
                table: "users",
                newName: "IdentityId");

            migrationBuilder.RenameIndex(
                name: "ix_users_identity_id",
                schema: "user",
                table: "users",
                newName: "IX_users_IdentityId");

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailVerified",
                schema: "user",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                schema: "user",
                table: "users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "currencies",
                schema: "currency",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currencies", x => x.Id);
                });
        }
    }
}
