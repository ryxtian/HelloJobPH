using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class applicantAndHrJoin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "HumanResource",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HumanResourceId",
                table: "Applicant",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_HumanResourceId",
                table: "Applicant",
                column: "HumanResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_HumanResource_HumanResourceId",
                table: "Applicant",
                column: "HumanResourceId",
                principalTable: "HumanResource",
                principalColumn: "HumanResourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_HumanResource_HumanResourceId",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_HumanResourceId",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "HumanResource");

            migrationBuilder.DropColumn(
                name: "HumanResourceId",
                table: "Applicant");
        }
    }
}
