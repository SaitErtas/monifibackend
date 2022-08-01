using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonifiBackend.API.Migrations
{
    public partial class SettingChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxSaleReferance",
                table: "Settings",
                newName: "MaximumSalesQuantity");

            migrationBuilder.RenameColumn(
                name: "MaxSaleApy",
                table: "Settings",
                newName: "MaximumReferenceGain");

            migrationBuilder.RenameColumn(
                name: "MaxSale",
                table: "Settings",
                newName: "MaximumDistributedAPY");

            migrationBuilder.AddColumn<decimal>(
                name: "MonifiPrice",
                table: "Settings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonifiPrice",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "MaximumSalesQuantity",
                table: "Settings",
                newName: "MaxSaleReferance");

            migrationBuilder.RenameColumn(
                name: "MaximumReferenceGain",
                table: "Settings",
                newName: "MaxSaleApy");

            migrationBuilder.RenameColumn(
                name: "MaximumDistributedAPY",
                table: "Settings",
                newName: "MaxSale");
        }
    }
}
