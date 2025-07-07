using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TenderManagementDAL.Migrations
{
    /// <inheritdoc />
    public partial class bidupdatemodelandrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Tenders_TenderId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Vendors_VendorId",
                table: "Bids");

            migrationBuilder.AddColumn<int>(
                name: "TenderId1",
                table: "Bids",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendorId1",
                table: "Bids",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bids_TenderId1",
                table: "Bids",
                column: "TenderId1");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_VendorId1",
                table: "Bids",
                column: "VendorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Tenders_TenderId",
                table: "Bids",
                column: "TenderId",
                principalTable: "Tenders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Tenders_TenderId1",
                table: "Bids",
                column: "TenderId1",
                principalTable: "Tenders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Vendors_VendorId",
                table: "Bids",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Vendors_VendorId1",
                table: "Bids",
                column: "VendorId1",
                principalTable: "Vendors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Tenders_TenderId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Tenders_TenderId1",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Vendors_VendorId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Vendors_VendorId1",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_TenderId1",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_VendorId1",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "TenderId1",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "VendorId1",
                table: "Bids");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Tenders_TenderId",
                table: "Bids",
                column: "TenderId",
                principalTable: "Tenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Vendors_VendorId",
                table: "Bids",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
