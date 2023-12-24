using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Users_And_Currencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "User",
                table: "Users");

            migrationBuilder.EnsureSchema(
                name: "currency");

            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "User",
                newName: "users",
                newSchema: "user");

            migrationBuilder.RenameIndex(
                name: "IX_Users_IdentityId",
                schema: "user",
                table: "users",
                newName: "IX_users_IdentityId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "currencies",
                schema: "currency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                schema: "user",
                table: "users");

            migrationBuilder.EnsureSchema(
                name: "User");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "user",
                newName: "Users",
                newSchema: "User");

            migrationBuilder.RenameIndex(
                name: "IX_users_IdentityId",
                schema: "User",
                table: "Users",
                newName: "IX_Users_IdentityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "User",
                table: "Users",
                column: "Id");
        }
    }
}
