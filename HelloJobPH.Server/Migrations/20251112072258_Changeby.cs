using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class Changeby : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPostAudit_JobPosting_JobPostingId",
                table: "JobPostAudit");

            migrationBuilder.DropColumn(
                name: "ChangedBy",
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

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostAudit_JobPosting_JobPostingId",
                table: "JobPostAudit",
                column: "JobPostingId",
                principalTable: "JobPosting",
                principalColumn: "JobPostingId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPostAudit_JobPosting_JobPostingId",
                table: "JobPostAudit");

            migrationBuilder.AlterColumn<int>(
                name: "JobPostingId",
                table: "JobPostAudit",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ChangedBy",
                table: "JobPostAudit",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostAudit_JobPosting_JobPostingId",
                table: "JobPostAudit",
                column: "JobPostingId",
                principalTable: "JobPosting",
                principalColumn: "JobPostingId");
        }
    }
}
