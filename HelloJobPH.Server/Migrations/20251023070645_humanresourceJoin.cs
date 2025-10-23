using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class humanresourceJoin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HumanResourceId",
                table: "JobPosting",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HumanResource",
                columns: table => new
                {
                    HumanResourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumanResource", x => x.HumanResourceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPosting_HumanResourceId",
                table: "JobPosting",
                column: "HumanResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosting_HumanResource_HumanResourceId",
                table: "JobPosting",
                column: "HumanResourceId",
                principalTable: "HumanResource",
                principalColumn: "HumanResourceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPosting_HumanResource_HumanResourceId",
                table: "JobPosting");

            migrationBuilder.DropTable(
                name: "HumanResource");

            migrationBuilder.DropIndex(
                name: "IX_JobPosting_HumanResourceId",
                table: "JobPosting");

            migrationBuilder.DropColumn(
                name: "HumanResourceId",
                table: "JobPosting");
        }
    }
}
