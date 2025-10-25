using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class ApplicantAndApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "Application",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Application_ApplicantId",
                table: "Application",
                column: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Applicant_ApplicantId",
                table: "Application",
                column: "ApplicantId",
                principalTable: "Applicant",
                principalColumn: "ApplicantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Applicant_ApplicantId",
                table: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Application_ApplicantId",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "Application");
        }
    }
}
