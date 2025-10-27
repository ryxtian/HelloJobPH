using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class InterviewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_JobPosting_JobPostingId",
                table: "Application");

            migrationBuilder.AlterColumn<int>(
                name: "JobPostingId",
                table: "Application",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Interview",
                columns: table => new
                {
                    InterviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Mode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    HumanResourceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interview", x => x.InterviewId);
                    table.ForeignKey(
                        name: "FK_Interview_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interview_HumanResource_HumanResourceId",
                        column: x => x.HumanResourceId,
                        principalTable: "HumanResource",
                        principalColumn: "HumanResourceId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interview_ApplicationId",
                table: "Interview",
                column: "ApplicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interview_HumanResourceId",
                table: "Interview",
                column: "HumanResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_JobPosting_JobPostingId",
                table: "Application",
                column: "JobPostingId",
                principalTable: "JobPosting",
                principalColumn: "JobPostingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_JobPosting_JobPostingId",
                table: "Application");

            migrationBuilder.DropTable(
                name: "Interview");

            migrationBuilder.AlterColumn<int>(
                name: "JobPostingId",
                table: "Application",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_JobPosting_JobPostingId",
                table: "Application",
                column: "JobPostingId",
                principalTable: "JobPosting",
                principalColumn: "JobPostingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
