using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAssignTo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Interview_InterviewerId",
                table: "Interview");

            migrationBuilder.DropColumn(
                name: "InterviewBy",
                table: "InterviewHistory");

            migrationBuilder.DropColumn(
                name: "AssignTo",
                table: "Interview");

            migrationBuilder.AddColumn<int>(
                name: "InterviewerId",
                table: "InterviewHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InterviewHistory_InterviewerId",
                table: "InterviewHistory",
                column: "InterviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_InterviewerId",
                table: "Interview",
                column: "InterviewerId");

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewHistory_Interviewer_InterviewerId",
                table: "InterviewHistory",
                column: "InterviewerId",
                principalTable: "Interviewer",
                principalColumn: "InterviewerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterviewHistory_Interviewer_InterviewerId",
                table: "InterviewHistory");

            migrationBuilder.DropIndex(
                name: "IX_InterviewHistory_InterviewerId",
                table: "InterviewHistory");

            migrationBuilder.DropIndex(
                name: "IX_Interview_InterviewerId",
                table: "Interview");

            migrationBuilder.DropColumn(
                name: "InterviewerId",
                table: "InterviewHistory");

            migrationBuilder.AddColumn<string>(
                name: "InterviewBy",
                table: "InterviewHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AssignTo",
                table: "Interview",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interview_InterviewerId",
                table: "Interview",
                column: "InterviewerId",
                unique: true,
                filter: "[InterviewerId] IS NOT NULL");
        }
    }
}
