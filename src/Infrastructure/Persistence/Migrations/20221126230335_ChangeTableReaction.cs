using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessagingSystemApp.Infrastructure.Persistence.Migrations
{
    public partial class ChangeTableReaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Reactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Reactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Reactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Reactions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Reactions");
        }
    }
}
