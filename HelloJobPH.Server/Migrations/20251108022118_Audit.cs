using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class Audit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignTo",
                table: "Interview",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    AuditLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: true),
                    JobPostingId = table.Column<int>(type: "int", nullable: true),
                    HumanResourcesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.AuditLogId);
                    table.ForeignKey(
                        name: "FK_AuditLog_Applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicant",
                        principalColumn: "ApplicantId");
                    table.ForeignKey(
                        name: "FK_AuditLog_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuditLog_HumanResource_HumanResourcesId",
                        column: x => x.HumanResourcesId,
                        principalTable: "HumanResource",
                        principalColumn: "HumanResourceId");
                    table.ForeignKey(
                        name: "FK_AuditLog_JobPosting_JobPostingId",
                        column: x => x.JobPostingId,
                        principalTable: "JobPosting",
                        principalColumn: "JobPostingId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_ApplicantId",
                table: "AuditLog",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_ApplicationId",
                table: "AuditLog",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_HumanResourcesId",
                table: "AuditLog",
                column: "HumanResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_JobPostingId",
                table: "AuditLog",
                column: "JobPostingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropColumn(
                name: "AssignTo",
                table: "Interview");
        }
    }
}
