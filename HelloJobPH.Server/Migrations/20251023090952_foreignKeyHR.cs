using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class foreignKeyHR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPosting_HumanResource_HumanResourceId",
                table: "JobPosting");

            migrationBuilder.AlterColumn<int>(
                name: "HumanResourceId",
                table: "JobPosting",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosting_HumanResource_HumanResourceId",
                table: "JobPosting",
                column: "HumanResourceId",
                principalTable: "HumanResource",
                principalColumn: "HumanResourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPosting_HumanResource_HumanResourceId",
                table: "JobPosting");

            migrationBuilder.AlterColumn<int>(
                name: "HumanResourceId",
                table: "JobPosting",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosting_HumanResource_HumanResourceId",
                table: "JobPosting",
                column: "HumanResourceId",
                principalTable: "HumanResource",
                principalColumn: "HumanResourceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
