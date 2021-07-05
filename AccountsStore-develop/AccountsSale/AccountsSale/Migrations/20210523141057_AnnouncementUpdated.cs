using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountsSale.Migrations
{
    public partial class AnnouncementUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "AnnouncementStatus",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "AnnouncementStatus");
        }
    }
}
