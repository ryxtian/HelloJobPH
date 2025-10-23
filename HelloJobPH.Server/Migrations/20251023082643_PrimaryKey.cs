using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class PrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_HumanResource_HumanResourceId",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_HumanResourceId",
                table: "Applicant");

            migrationBuilder.AddColumn<int>(
                name: "HumanResourcesHumanResourceId",
                table: "Applicant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_HumanResourcesHumanResourceId",
                table: "Applicant",
                column: "HumanResourcesHumanResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_HumanResource_HumanResourcesHumanResourceId",
                table: "Applicant",
                column: "HumanResourcesHumanResourceId",
                principalTable: "HumanResource",
                principalColumn: "HumanResourceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_HumanResource_HumanResourcesHumanResourceId",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_HumanResourcesHumanResourceId",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "HumanResourcesHumanResourceId",
                table: "Applicant");

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
    }
}
