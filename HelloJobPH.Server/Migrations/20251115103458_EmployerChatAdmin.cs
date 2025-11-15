using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class EmployerChatAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployerId",
                table: "ChatMessages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_EmployerId",
                table: "ChatMessages",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Employer_EmployerId",
                table: "ChatMessages",
                column: "EmployerId",
                principalTable: "Employer",
                principalColumn: "EmployerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Employer_EmployerId",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_EmployerId",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "ChatMessages");
        }
    }
}
