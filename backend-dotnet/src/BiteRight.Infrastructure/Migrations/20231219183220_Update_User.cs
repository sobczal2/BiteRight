using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Auth0Id",
                schema: "User",
                table: "Users",
                newName: "IdentityId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "User",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailVerified",
                schema: "User",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinedAt",
                schema: "User",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdentityId",
                schema: "User",
                table: "Users",
                column: "IdentityId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_IdentityId",
                schema: "User",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "User",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsEmailVerified",
                schema: "User",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "JoinedAt",
                schema: "User",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "IdentityId",
                schema: "User",
                table: "Users",
                newName: "Auth0Id");
        }
    }
}
