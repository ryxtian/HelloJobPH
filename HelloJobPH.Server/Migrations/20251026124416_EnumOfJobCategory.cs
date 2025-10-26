using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class EnumOfJobCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "HumanResource");

            migrationBuilder.AddColumn<int>(
                name: "JobCategory",
                table: "JobPosting",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobCategory",
                table: "JobPosting");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "HumanResource",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
