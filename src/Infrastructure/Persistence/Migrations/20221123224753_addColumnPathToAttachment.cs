using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessagingSystemApp.Infrastructure.Persistence.Migrations
{
    public partial class addColumnPathToAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Attachments");
        }
    }
}
