using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class interviewer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InterviewerId",
                table: "Interview",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Interviewer",
                columns: table => new
                {
                    InterviewerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviewer", x => x.InterviewerId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interview_InterviewerId",
                table: "Interview",
                column: "InterviewerId",
                unique: true,
                filter: "[InterviewerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Interview_Interviewer_InterviewerId",
                table: "Interview",
                column: "InterviewerId",
                principalTable: "Interviewer",
                principalColumn: "InterviewerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interview_Interviewer_InterviewerId",
                table: "Interview");

            migrationBuilder.DropTable(
                name: "Interviewer");

            migrationBuilder.DropIndex(
                name: "IX_Interview_InterviewerId",
                table: "Interview");

            migrationBuilder.DropColumn(
                name: "InterviewerId",
                table: "Interview");
        }
    }
}
