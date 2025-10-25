using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class ApplicantAndApplication2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Applicant_ApplicantId",
                table: "Application");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicantId",
                table: "Application",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Applicant_ApplicantId",
                table: "Application",
                column: "ApplicantId",
                principalTable: "Applicant",
                principalColumn: "ApplicantId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Applicant_ApplicantId",
                table: "Application");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicantId",
                table: "Application",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Applicant_ApplicantId",
                table: "Application",
                column: "ApplicantId",
                principalTable: "Applicant",
                principalColumn: "ApplicantId");
        }
    }
}
