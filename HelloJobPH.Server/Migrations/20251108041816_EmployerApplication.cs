using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class EmployerApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployerId",
                table: "Application",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Application_EmployerId",
                table: "Application",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Employer_EmployerId",
                table: "Application",
                column: "EmployerId",
                principalTable: "Employer",
                principalColumn: "EmployerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Employer_EmployerId",
                table: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Application_EmployerId",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "Application");
        }
    }
}
