using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChatJoinHRAndApplicant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "ChatMessages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HumanResourcesId",
                table: "ChatMessages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ApplicantId",
                table: "ChatMessages",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_HumanResourcesId",
                table: "ChatMessages",
                column: "HumanResourcesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Applicant_ApplicantId",
                table: "ChatMessages",
                column: "ApplicantId",
                principalTable: "Applicant",
                principalColumn: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_HumanResource_HumanResourcesId",
                table: "ChatMessages",
                column: "HumanResourcesId",
                principalTable: "HumanResource",
                principalColumn: "HumanResourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Applicant_ApplicantId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_HumanResource_HumanResourcesId",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_ApplicantId",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_HumanResourcesId",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "HumanResourcesId",
                table: "ChatMessages");
        }
    }
}
