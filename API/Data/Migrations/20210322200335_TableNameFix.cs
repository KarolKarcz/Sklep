using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class TableNameFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserPersonalData_UserAddress_AdressId",
                table: "AppUserPersonalData");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailConfirmation_Users_UserId",
                table: "EmailConfirmation");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AppUserPersonalData_PersonalDataId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "AppUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PersonalDataId",
                table: "AppUsers",
                newName: "IX_AppUsers_PersonalDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AppUserAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TelephoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    StreetAndHouseNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    Nip = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserAddress", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserPersonalData_AppUserAddress_AdressId",
                table: "AppUserPersonalData",
                column: "AdressId",
                principalTable: "AppUserAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_AppUserPersonalData_PersonalDataId",
                table: "AppUsers",
                column: "PersonalDataId",
                principalTable: "AppUserPersonalData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailConfirmation_AppUsers_UserId",
                table: "EmailConfirmation",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserPersonalData_AppUserAddress_AdressId",
                table: "AppUserPersonalData");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AppUserPersonalData_PersonalDataId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailConfirmation_AppUsers_UserId",
                table: "EmailConfirmation");

            migrationBuilder.DropTable(
                name: "AppUserAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers");

            migrationBuilder.RenameTable(
                name: "AppUsers",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_PersonalDataId",
                table: "Users",
                newName: "IX_Users_PersonalDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    Nip = table.Column<string>(type: "TEXT", nullable: true),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: true),
                    StreetAndHouseNumber = table.Column<string>(type: "TEXT", nullable: true),
                    TelephoneNumber = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddress", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserPersonalData_UserAddress_AdressId",
                table: "AppUserPersonalData",
                column: "AdressId",
                principalTable: "UserAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailConfirmation_Users_UserId",
                table: "EmailConfirmation",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AppUserPersonalData_PersonalDataId",
                table: "Users",
                column: "PersonalDataId",
                principalTable: "AppUserPersonalData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
