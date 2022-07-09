using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonifiBackend.API.Migrations
{
    public partial class WalletModuleFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Commission",
                table: "Packages",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Commission",
                table: "Packages",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
