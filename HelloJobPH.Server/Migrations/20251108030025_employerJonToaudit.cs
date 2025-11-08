using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class employerJonToaudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployerId",
                table: "AuditLog",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_EmployerId",
                table: "AuditLog",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLog_Employer_EmployerId",
                table: "AuditLog",
                column: "EmployerId",
                principalTable: "Employer",
                principalColumn: "EmployerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLog_Employer_EmployerId",
                table: "AuditLog");

            migrationBuilder.DropIndex(
                name: "IX_AuditLog_EmployerId",
                table: "AuditLog");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "AuditLog");
        }
    }
}
