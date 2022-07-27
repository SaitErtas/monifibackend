using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonifiBackend.API.Migrations
{
    public partial class InitialAbc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangePeriodDay",
                table: "PackageDetails");

            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "PackageDetails");

            migrationBuilder.DropColumn(
                name: "MinValue",
                table: "PackageDetails");

            migrationBuilder.AddColumn<int>(
                name: "ChangePeriodDay",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxValue",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinValue",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangePeriodDay",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "MinValue",
                table: "Packages");

            migrationBuilder.AddColumn<int>(
                name: "ChangePeriodDay",
                table: "PackageDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxValue",
                table: "PackageDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinValue",
                table: "PackageDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
