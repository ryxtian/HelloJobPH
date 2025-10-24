using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobPH.Server.Migrations
{
    /// <inheritdoc />
    public partial class userAcctsDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_HumanResource_HumanResourcesHumanResourceId",
                table: "Applicant");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccount_Applicant_ApplicantId",
                table: "UserAccount");

            migrationBuilder.DropIndex(
                name: "IX_UserAccount_ApplicantId",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "UserAccount");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "UserAccount",
                newName: "userDetailsId");

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "HumanResource",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "HumanResourcesHumanResourceId",
                table: "Applicant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "Applicant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HumanResource_UserAccountId",
                table: "HumanResource",
                column: "UserAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_UserAccountId",
                table: "Applicant",
                column: "UserAccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_HumanResource_HumanResourcesHumanResourceId",
                table: "Applicant",
                column: "HumanResourcesHumanResourceId",
                principalTable: "HumanResource",
                principalColumn: "HumanResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_UserAccount_UserAccountId",
                table: "Applicant",
                column: "UserAccountId",
                principalTable: "UserAccount",
                principalColumn: "UserAccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HumanResource_UserAccount_UserAccountId",
                table: "HumanResource",
                column: "UserAccountId",
                principalTable: "UserAccount",
                principalColumn: "UserAccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_HumanResource_HumanResourcesHumanResourceId",
                table: "Applicant");

            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_UserAccount_UserAccountId",
                table: "Applicant");

            migrationBuilder.DropForeignKey(
                name: "FK_HumanResource_UserAccount_UserAccountId",
                table: "HumanResource");

            migrationBuilder.DropIndex(
                name: "IX_HumanResource_UserAccountId",
                table: "HumanResource");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_UserAccountId",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "HumanResource");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "Applicant");

            migrationBuilder.RenameColumn(
                name: "userDetailsId",
                table: "UserAccount",
                newName: "ApplicantId");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "UserAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "HumanResourcesHumanResourceId",
                table: "Applicant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_ApplicantId",
                table: "UserAccount",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_HumanResource_HumanResourcesHumanResourceId",
                table: "Applicant",
                column: "HumanResourcesHumanResourceId",
                principalTable: "HumanResource",
                principalColumn: "HumanResourceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccount_Applicant_ApplicantId",
                table: "UserAccount",
                column: "ApplicantId",
                principalTable: "Applicant",
                principalColumn: "ApplicantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
