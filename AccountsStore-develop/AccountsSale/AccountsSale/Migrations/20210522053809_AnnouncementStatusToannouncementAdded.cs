using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountsSale.Migrations
{
    public partial class AnnouncementStatusToannouncementAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPaid",
                table: "Announcements",
                newName: "IsVip");

            migrationBuilder.AddColumn<string>(
                name: "AccountEmail",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountPassword",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AnnouncementStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsChecked = table.Column<bool>(type: "bit", nullable: false),
                    AnnouncementId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementStatus_Announcements_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "Announcements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementStatus_AnnouncementId",
                table: "AnnouncementStatus",
                column: "AnnouncementId",
                unique: true,
                filter: "[AnnouncementId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnouncementStatus");

            migrationBuilder.DropColumn(
                name: "AccountEmail",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "AccountPassword",
                table: "Announcements");

            migrationBuilder.RenameColumn(
                name: "IsVip",
                table: "Announcements",
                newName: "IsPaid");
        }
    }
}
