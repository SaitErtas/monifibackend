using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonifiBackend.API.Migrations
{
    public partial class DatabaseRevision2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountMovements_WalletEntity_WalletId",
                table: "AccountMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserIPEntity_Users_UserId",
                table: "UserIPEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_CountryEntity_CountryId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_LanguageEntity_LanguageId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletEntity_NetworkEntity_CryptoNetworkId",
                table: "WalletEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletEntity_Users_UserId",
                table: "WalletEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalletEntity",
                table: "WalletEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserIPEntity",
                table: "UserIPEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NetworkEntity",
                table: "NetworkEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LanguageEntity",
                table: "LanguageEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryEntity",
                table: "CountryEntity");

            migrationBuilder.RenameTable(
                name: "WalletEntity",
                newName: "Wallets");

            migrationBuilder.RenameTable(
                name: "UserIPEntity",
                newName: "UserIPs");

            migrationBuilder.RenameTable(
                name: "NetworkEntity",
                newName: "Networks");

            migrationBuilder.RenameTable(
                name: "LanguageEntity",
                newName: "Languages");

            migrationBuilder.RenameTable(
                name: "CountryEntity",
                newName: "Countries");

            migrationBuilder.RenameIndex(
                name: "IX_WalletEntity_UserId",
                table: "Wallets",
                newName: "IX_Wallets_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WalletEntity_CryptoNetworkId",
                table: "Wallets",
                newName: "IX_Wallets_CryptoNetworkId");

            migrationBuilder.RenameIndex(
                name: "IX_UserIPEntity_UserId",
                table: "UserIPs",
                newName: "IX_UserIPs_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "WalletAddress",
                table: "Wallets",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequestName",
                table: "UserIPs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "UserIPs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Networks",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                table: "Languages",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NativeName",
                table: "Languages",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Languages",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Countries",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Iso3",
                table: "Countries",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Iso2",
                table: "Countries",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Flag",
                table: "Countries",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserIPs",
                table: "UserIPs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Networks",
                table: "Networks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountMovements_Wallets_WalletId",
                table: "AccountMovements",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserIPs_Users_UserId",
                table: "UserIPs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Countries_CountryId",
                table: "Users",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Languages_LanguageId",
                table: "Users",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Networks_CryptoNetworkId",
                table: "Wallets",
                column: "CryptoNetworkId",
                principalTable: "Networks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Users_UserId",
                table: "Wallets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountMovements_Wallets_WalletId",
                table: "AccountMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserIPs_Users_UserId",
                table: "UserIPs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Countries_CountryId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Languages_LanguageId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Networks_CryptoNetworkId",
                table: "Wallets");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Users_UserId",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserIPs",
                table: "UserIPs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Networks",
                table: "Networks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.RenameTable(
                name: "Wallets",
                newName: "WalletEntity");

            migrationBuilder.RenameTable(
                name: "UserIPs",
                newName: "UserIPEntity");

            migrationBuilder.RenameTable(
                name: "Networks",
                newName: "NetworkEntity");

            migrationBuilder.RenameTable(
                name: "Languages",
                newName: "LanguageEntity");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "CountryEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Wallets_UserId",
                table: "WalletEntity",
                newName: "IX_WalletEntity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Wallets_CryptoNetworkId",
                table: "WalletEntity",
                newName: "IX_WalletEntity_CryptoNetworkId");

            migrationBuilder.RenameIndex(
                name: "IX_UserIPs_UserId",
                table: "UserIPEntity",
                newName: "IX_UserIPEntity_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "WalletAddress",
                table: "WalletEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "RequestName",
                table: "UserIPEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "UserIPEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NetworkEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                table: "LanguageEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "NativeName",
                table: "LanguageEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LanguageEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CountryEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Iso3",
                table: "CountryEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Iso2",
                table: "CountryEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Flag",
                table: "CountryEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalletEntity",
                table: "WalletEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserIPEntity",
                table: "UserIPEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NetworkEntity",
                table: "NetworkEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LanguageEntity",
                table: "LanguageEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryEntity",
                table: "CountryEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountMovements_WalletEntity_WalletId",
                table: "AccountMovements",
                column: "WalletId",
                principalTable: "WalletEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserIPEntity_Users_UserId",
                table: "UserIPEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CountryEntity_CountryId",
                table: "Users",
                column: "CountryId",
                principalTable: "CountryEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LanguageEntity_LanguageId",
                table: "Users",
                column: "LanguageId",
                principalTable: "LanguageEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletEntity_NetworkEntity_CryptoNetworkId",
                table: "WalletEntity",
                column: "CryptoNetworkId",
                principalTable: "NetworkEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletEntity_Users_UserId",
                table: "WalletEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
