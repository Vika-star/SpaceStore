using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountsSale.Migrations
{
    public partial class TransferIsPaidToAnnoucementFromAnnouncementStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "AnnouncementStatus");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Announcements",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Announcements");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "AnnouncementStatus",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
