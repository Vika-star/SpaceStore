using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountsSale.Migrations
{
    public partial class CascadeDeleteBehaviourAnnouncementAnnouncementStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementStatus_Announcements_AnnouncementId",
                table: "AnnouncementStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementStatus_Announcements_AnnouncementId",
                table: "AnnouncementStatus",
                column: "AnnouncementId",
                principalTable: "Announcements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementStatus_Announcements_AnnouncementId",
                table: "AnnouncementStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementStatus_Announcements_AnnouncementId",
                table: "AnnouncementStatus",
                column: "AnnouncementId",
                principalTable: "Announcements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
