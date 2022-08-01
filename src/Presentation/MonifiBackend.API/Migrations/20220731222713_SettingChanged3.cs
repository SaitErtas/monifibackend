using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonifiBackend.API.Migrations
{
    public partial class SettingChanged3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaximumReferenceGain",
                table: "Settings",
                newName: "TotalPreSaleQuantity");

            migrationBuilder.AddColumn<long>(
                name: "MaximumReferenceBonus",
                table: "Settings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumReferenceBonus",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "TotalPreSaleQuantity",
                table: "Settings",
                newName: "MaximumReferenceGain");
        }
    }
}
