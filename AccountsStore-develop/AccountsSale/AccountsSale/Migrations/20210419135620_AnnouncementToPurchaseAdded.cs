using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountsSale.Migrations
{
    public partial class AnnouncementToPurchaseAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "Announcements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_PurchaseId",
                table: "Announcements",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Purchases_PurchaseId",
                table: "Announcements",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Purchases_PurchaseId",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_PurchaseId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Announcements");
        }
    }
}
