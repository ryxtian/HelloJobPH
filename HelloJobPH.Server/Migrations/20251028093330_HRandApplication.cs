using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class HRandApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HumanResourceId",
                table: "Application",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HumanResourcesHumanResourceId",
                table: "Application",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Application_HumanResourcesHumanResourceId",
                table: "Application",
                column: "HumanResourcesHumanResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_HumanResource_HumanResourcesHumanResourceId",
                table: "Application",
                column: "HumanResourcesHumanResourceId",
                principalTable: "HumanResource",
                principalColumn: "HumanResourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_HumanResource_HumanResourcesHumanResourceId",
                table: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Application_HumanResourcesHumanResourceId",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "HumanResourceId",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "HumanResourcesHumanResourceId",
                table: "Application");
        }
    }
}
