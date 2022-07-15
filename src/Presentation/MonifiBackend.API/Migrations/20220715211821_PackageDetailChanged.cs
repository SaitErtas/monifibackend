using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonifiBackend.API.Migrations
{
    public partial class PackageDetailChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackageDetailEntity_Packages_PackageId",
                table: "PackageDetailEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackageDetailEntity",
                table: "PackageDetailEntity");

            migrationBuilder.RenameTable(
                name: "PackageDetailEntity",
                newName: "PackageDetails");

            migrationBuilder.RenameIndex(
                name: "IX_PackageDetailEntity_PackageId",
                table: "PackageDetails",
                newName: "IX_PackageDetails_PackageId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PackageDetails",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackageDetails",
                table: "PackageDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PackageDetails_Packages_PackageId",
                table: "PackageDetails",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackageDetails_Packages_PackageId",
                table: "PackageDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackageDetails",
                table: "PackageDetails");

            migrationBuilder.DropColumn(
                name: "ChangePeriodDay",
                table: "PackageDetails");

            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "PackageDetails");

            migrationBuilder.DropColumn(
                name: "MinValue",
                table: "PackageDetails");

            migrationBuilder.RenameTable(
                name: "PackageDetails",
                newName: "PackageDetailEntity");

            migrationBuilder.RenameIndex(
                name: "IX_PackageDetails_PackageId",
                table: "PackageDetailEntity",
                newName: "IX_PackageDetailEntity_PackageId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PackageDetailEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackageDetailEntity",
                table: "PackageDetailEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PackageDetailEntity_Packages_PackageId",
                table: "PackageDetailEntity",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
