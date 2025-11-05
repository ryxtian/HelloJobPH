using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class foreignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPosting_Employers_EmployersId",
                table: "JobPosting");

            migrationBuilder.RenameColumn(
                name: "EmployersId",
                table: "JobPosting",
                newName: "EmployersEmployerId");

            migrationBuilder.RenameIndex(
                name: "IX_JobPosting_EmployersId",
                table: "JobPosting",
                newName: "IX_JobPosting_EmployersEmployerId");

            migrationBuilder.AddColumn<int>(
                name: "EmployerId",
                table: "JobPosting",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosting_Employers_EmployersEmployerId",
                table: "JobPosting",
                column: "EmployersEmployerId",
                principalTable: "Employers",
                principalColumn: "EmployerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPosting_Employers_EmployersEmployerId",
                table: "JobPosting");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "JobPosting");

            migrationBuilder.RenameColumn(
                name: "EmployersEmployerId",
                table: "JobPosting",
                newName: "EmployersId");

            migrationBuilder.RenameIndex(
                name: "IX_JobPosting_EmployersEmployerId",
                table: "JobPosting",
                newName: "IX_JobPosting_EmployersId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosting_Employers_EmployersId",
                table: "JobPosting",
                column: "EmployersId",
                principalTable: "Employers",
                principalColumn: "EmployerId");
        }
    }
}
