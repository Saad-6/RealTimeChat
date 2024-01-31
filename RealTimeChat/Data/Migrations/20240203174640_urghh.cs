using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class urghh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThisUsersId",
                table: "Chats");

            migrationBuilder.AddColumn<string>(
                name: "SenderUserId",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_SenderUserId",
                table: "Chats",
                column: "SenderUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_SenderUserId",
                table: "Chats",
                column: "SenderUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_SenderUserId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_SenderUserId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "SenderUserId",
                table: "Chats");

            migrationBuilder.AddColumn<string>(
                name: "ThisUsersId",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
