using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class CoverLetter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResumeUrl",
                table: "Application",
                newName: "CoverLetter");

            migrationBuilder.AddColumn<int>(
                name: "ResumeId",
                table: "Application",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "Application");

            migrationBuilder.RenameColumn(
                name: "CoverLetter",
                table: "Application",
                newName: "ResumeUrl");
        }
    }
}
