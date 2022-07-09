using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonifiBackend.API.Migrations
{
    public partial class WalletModuleFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountMovementEntity_Packages_PackageId",
                table: "AccountMovementEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountMovementEntity_Users_UserId",
                table: "AccountMovementEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountMovementEntity",
                table: "AccountMovementEntity");

            migrationBuilder.RenameTable(
                name: "AccountMovementEntity",
                newName: "AccountMovements");

            migrationBuilder.RenameIndex(
                name: "IX_AccountMovementEntity_UserId",
                table: "AccountMovements",
                newName: "IX_AccountMovements_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountMovementEntity_PackageId",
                table: "AccountMovements",
                newName: "IX_AccountMovements_PackageId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Packages",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Commission",
                table: "Packages",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "AccountMovements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountMovements",
                table: "AccountMovements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountMovements_Packages_PackageId",
                table: "AccountMovements",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountMovements_Users_UserId",
                table: "AccountMovements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountMovements_Packages_PackageId",
                table: "AccountMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountMovements_Users_UserId",
                table: "AccountMovements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountMovements",
                table: "AccountMovements");

            migrationBuilder.RenameTable(
                name: "AccountMovements",
                newName: "AccountMovementEntity");

            migrationBuilder.RenameIndex(
                name: "IX_AccountMovements_UserId",
                table: "AccountMovementEntity",
                newName: "IX_AccountMovementEntity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountMovements_PackageId",
                table: "AccountMovementEntity",
                newName: "IX_AccountMovementEntity_PackageId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Packages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<decimal>(
                name: "Commission",
                table: "Packages",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "AccountMovementEntity",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountMovementEntity",
                table: "AccountMovementEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountMovementEntity_Packages_PackageId",
                table: "AccountMovementEntity",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountMovementEntity_Users_UserId",
                table: "AccountMovementEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
