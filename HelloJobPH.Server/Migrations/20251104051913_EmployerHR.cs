using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class EmployerHR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployerId",
                table: "HumanResource",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployersEmployerId",
                table: "HumanResource",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HumanResource_EmployersEmployerId",
                table: "HumanResource",
                column: "EmployersEmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_HumanResource_Employers_EmployersEmployerId",
                table: "HumanResource",
                column: "EmployersEmployerId",
                principalTable: "Employers",
                principalColumn: "EmployerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HumanResource_Employers_EmployersEmployerId",
                table: "HumanResource");

            migrationBuilder.DropIndex(
                name: "IX_HumanResource_EmployersEmployerId",
                table: "HumanResource");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "HumanResource");

            migrationBuilder.DropColumn(
                name: "EmployersEmployerId",
                table: "HumanResource");
        }
    }
}
