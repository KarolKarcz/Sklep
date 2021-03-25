using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class AddedMoreDetailedUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "emailAddres",
                table: "PasswordReset",
                newName: "EmailAddres");

            migrationBuilder.AddColumn<int>(
                name: "PersonalDataId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppUserPersonalData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Newsletter = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserPersonalData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    TelephoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    StreetAndHouseNumber = table.Column<string>(type: "TEXT", nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    Nip = table.Column<string>(type: "TEXT", nullable: true),
                    AppUserPersonalDataId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddresses_AppUserPersonalData_AppUserPersonalDataId",
                        column: x => x.AppUserPersonalDataId,
                        principalTable: "AppUserPersonalData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonalDataId",
                table: "Users",
                column: "PersonalDataId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_AppUserPersonalDataId",
                table: "UserAddresses",
                column: "AppUserPersonalDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AppUserPersonalData_PersonalDataId",
                table: "Users",
                column: "PersonalDataId",
                principalTable: "AppUserPersonalData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AppUserPersonalData_PersonalDataId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserAddresses");

            migrationBuilder.DropTable(
                name: "AppUserPersonalData");

            migrationBuilder.DropIndex(
                name: "IX_Users_PersonalDataId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PersonalDataId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "EmailAddres",
                table: "PasswordReset",
                newName: "emailAddres");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }
    }
}
