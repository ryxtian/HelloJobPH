using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class JobPostAndEmployer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployersId",
                table: "JobPosting",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobPosting_EmployersId",
                table: "JobPosting",
                column: "EmployersId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosting_Employers_EmployersId",
                table: "JobPosting",
                column: "EmployersId",
                principalTable: "Employers",
                principalColumn: "EmployerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPosting_Employers_EmployersId",
                table: "JobPosting");

            migrationBuilder.DropIndex(
                name: "IX_JobPosting_EmployersId",
                table: "JobPosting");

            migrationBuilder.DropColumn(
                name: "EmployersId",
                table: "JobPosting");
        }
    }
}
