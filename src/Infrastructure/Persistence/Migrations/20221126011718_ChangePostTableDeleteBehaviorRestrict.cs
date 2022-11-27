using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessagingSystemApp.Infrastructure.Persistence.Migrations
{
    public partial class ChangePostTableDeleteBehaviorRestrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Posts_ReplyPostId",
                table: "Posts");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Posts_ReplyPostId",
                table: "Posts",
                column: "ReplyPostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Posts_ReplyPostId",
                table: "Posts");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Posts_ReplyPostId",
                table: "Posts",
                column: "ReplyPostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
