using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonifiBackend.API.Migrations
{
    public partial class DatabaseRevision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountMovements_Users_UserId",
                table: "AccountMovements");

            migrationBuilder.DropColumn(
                name: "PhoneType",
                table: "UserPhones");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AccountMovements",
                newName: "WalletId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountMovements_UserId",
                table: "AccountMovements",
                newName: "IX_AccountMovements_WalletId");

            migrationBuilder.AlterColumn<string>(
                name: "ResetPasswordCode",
                table: "Users",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ReferanceCode",
                table: "Users",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmationCode",
                table: "Users",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CountryEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iso2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iso3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NativeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NetworkEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserIPEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserIPEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserIPEntity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WalletAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CryptoNetworkId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletEntity_NetworkEntity_CryptoNetworkId",
                        column: x => x.CryptoNetworkId,
                        principalTable: "NetworkEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WalletEntity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryId",
                table: "Users",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LanguageId",
                table: "Users",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserIPEntity_UserId",
                table: "UserIPEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletEntity_CryptoNetworkId",
                table: "WalletEntity",
                column: "CryptoNetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletEntity_UserId",
                table: "WalletEntity",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountMovements_WalletEntity_WalletId",
                table: "AccountMovements",
                column: "WalletId",
                principalTable: "WalletEntity",
                principalColumn: "Id");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountMovements_WalletEntity_WalletId",
                table: "AccountMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_CountryEntity_CountryId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_LanguageEntity_LanguageId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "CountryEntity");

            migrationBuilder.DropTable(
                name: "LanguageEntity");

            migrationBuilder.DropTable(
                name: "UserIPEntity");

            migrationBuilder.DropTable(
                name: "WalletEntity");

            migrationBuilder.DropTable(
                name: "NetworkEntity");

            migrationBuilder.DropIndex(
                name: "IX_Users_CountryId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_LanguageId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "WalletId",
                table: "AccountMovements",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountMovements_WalletId",
                table: "AccountMovements",
                newName: "IX_AccountMovements_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "ResetPasswordCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReferanceCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmationCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhoneType",
                table: "UserPhones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountMovements_Users_UserId",
                table: "AccountMovements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
