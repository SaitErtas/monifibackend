using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonifiBackend.API.Migrations
{
    public partial class PackageDetailChanged2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountMovements_Packages_PackageId",
                table: "AccountMovements");

            migrationBuilder.RenameColumn(
                name: "PackageId",
                table: "AccountMovements",
                newName: "PackageDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountMovements_PackageId",
                table: "AccountMovements",
                newName: "IX_AccountMovements_PackageDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountMovements_PackageDetails_PackageDetailId",
                table: "AccountMovements",
                column: "PackageDetailId",
                principalTable: "PackageDetails",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountMovements_PackageDetails_PackageDetailId",
                table: "AccountMovements");

            migrationBuilder.RenameColumn(
                name: "PackageDetailId",
                table: "AccountMovements",
                newName: "PackageId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountMovements_PackageDetailId",
                table: "AccountMovements",
                newName: "IX_AccountMovements_PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountMovements_Packages_PackageId",
                table: "AccountMovements",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id");
        }
    }
}
