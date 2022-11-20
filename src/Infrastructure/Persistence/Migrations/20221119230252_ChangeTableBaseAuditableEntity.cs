using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessagingSystemApp.Infrastructure.Persistence.Migrations
{
    public partial class ChangeTableBaseAuditableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Connections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Connections",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Connections");
        }
    }
}
