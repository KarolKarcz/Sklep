using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class DatabaseTweaks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailConfirmation_Users_UserIdId",
                table: "EmailConfirmation");

            migrationBuilder.RenameColumn(
                name: "UserIdId",
                table: "EmailConfirmation",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailConfirmation_UserIdId",
                table: "EmailConfirmation",
                newName: "IX_EmailConfirmation_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailConfirmation_Users_UserId",
                table: "EmailConfirmation",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailConfirmation_Users_UserId",
                table: "EmailConfirmation");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "EmailConfirmation",
                newName: "UserIdId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailConfirmation_UserId",
                table: "EmailConfirmation",
                newName: "IX_EmailConfirmation_UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailConfirmation_Users_UserIdId",
                table: "EmailConfirmation",
                column: "UserIdId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
