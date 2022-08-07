using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonifiBackend.API.Migrations
{
    public partial class MoventNewAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MonifiPrice",
                table: "Settings",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "BscScanAddress",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BscScanTokenSymbol",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TronNetworkAddress",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TronNetworkTokenSymbol",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "AccountMovements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TokenSymbol",
                table: "AccountMovements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "TransferTime",
                table: "AccountMovements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BscScanAddress",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "BscScanTokenSymbol",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "TronNetworkAddress",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "TronNetworkTokenSymbol",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Hash",
                table: "AccountMovements");

            migrationBuilder.DropColumn(
                name: "TokenSymbol",
                table: "AccountMovements");

            migrationBuilder.DropColumn(
                name: "TransferTime",
                table: "AccountMovements");

            migrationBuilder.AlterColumn<decimal>(
                name: "MonifiPrice",
                table: "Settings",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5);
        }
    }
}
