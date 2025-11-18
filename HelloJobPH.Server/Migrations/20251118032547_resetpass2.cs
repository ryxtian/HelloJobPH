using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class resetpass2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PasswordResetTokens_UserAccount_UserAccountId",
                table: "PasswordResetTokens");

            migrationBuilder.DropIndex(
                name: "IX_PasswordResetTokens_UserAccountId",
                table: "PasswordResetTokens");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PasswordResetTokens");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "PasswordResetTokens");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "PasswordResetTokens");

            migrationBuilder.RenameColumn(
                name: "ExpiresAt",
                table: "PasswordResetTokens",
                newName: "Expiration");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PasswordResetTokens",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "PasswordResetTokens");

            migrationBuilder.RenameColumn(
                name: "Expiration",
                table: "PasswordResetTokens",
                newName: "ExpiresAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PasswordResetTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "PasswordResetTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "PasswordResetTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_UserAccountId",
                table: "PasswordResetTokens",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordResetTokens_UserAccount_UserAccountId",
                table: "PasswordResetTokens",
                column: "UserAccountId",
                principalTable: "UserAccount",
                principalColumn: "UserAccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
