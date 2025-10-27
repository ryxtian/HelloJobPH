using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class nullableApplicationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interview_Application_ApplicationId",
                table: "Interview");

            migrationBuilder.DropIndex(
                name: "IX_Interview_ApplicationId",
                table: "Interview");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationId",
                table: "Interview",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_ApplicationId",
                table: "Interview",
                column: "ApplicationId",
                unique: true,
                filter: "[ApplicationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Interview_Application_ApplicationId",
                table: "Interview",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "ApplicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interview_Application_ApplicationId",
                table: "Interview");

            migrationBuilder.DropIndex(
                name: "IX_Interview_ApplicationId",
                table: "Interview");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationId",
                table: "Interview",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interview_ApplicationId",
                table: "Interview",
                column: "ApplicationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Interview_Application_ApplicationId",
                table: "Interview",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
