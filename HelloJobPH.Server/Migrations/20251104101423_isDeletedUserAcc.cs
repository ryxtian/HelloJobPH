using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class isDeletedUserAcc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLoggedIn",
                table: "UserAccount");

            migrationBuilder.AddColumn<byte>(
                name: "IsDeleted",
                table: "UserAccount",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserAccount");

            migrationBuilder.AddColumn<bool>(
                name: "IsLoggedIn",
                table: "UserAccount",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
