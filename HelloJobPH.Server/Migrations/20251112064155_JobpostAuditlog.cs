using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class JobpostAuditlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "JobPostingId",
                table: "JobPostAudit",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EmployerId",
                table: "JobPostAudit",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HumanResourceId",
                table: "JobPostAudit",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobPostAudit_EmployerId",
                table: "JobPostAudit",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostAudit_HumanResourceId",
                table: "JobPostAudit",
                column: "HumanResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostAudit_JobPostingId",
                table: "JobPostAudit",
                column: "JobPostingId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostAudit_Employer_EmployerId",
                table: "JobPostAudit",
                column: "EmployerId",
                principalTable: "Employer",
                principalColumn: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostAudit_HumanResource_HumanResourceId",
                table: "JobPostAudit",
                column: "HumanResourceId",
                principalTable: "HumanResource",
                principalColumn: "HumanResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostAudit_JobPosting_JobPostingId",
                table: "JobPostAudit",
                column: "JobPostingId",
                principalTable: "JobPosting",
                principalColumn: "JobPostingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPostAudit_Employer_EmployerId",
                table: "JobPostAudit");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPostAudit_HumanResource_HumanResourceId",
                table: "JobPostAudit");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPostAudit_JobPosting_JobPostingId",
                table: "JobPostAudit");

            migrationBuilder.DropIndex(
                name: "IX_JobPostAudit_EmployerId",
                table: "JobPostAudit");

            migrationBuilder.DropIndex(
                name: "IX_JobPostAudit_HumanResourceId",
                table: "JobPostAudit");

            migrationBuilder.DropIndex(
                name: "IX_JobPostAudit_JobPostingId",
                table: "JobPostAudit");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "JobPostAudit");

            migrationBuilder.DropColumn(
                name: "HumanResourceId",
                table: "JobPostAudit");

            migrationBuilder.AlterColumn<int>(
                name: "JobPostingId",
                table: "JobPostAudit",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
