using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class newone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParticipantIds",
                table: "Chats",
                newName: "ThisUsersId");

            migrationBuilder.AddColumn<string>(
                name: "OtherUserId",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_OtherUserId",
                table: "Chats",
                column: "OtherUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_OtherUserId",
                table: "Chats",
                column: "OtherUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_OtherUserId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_OtherUserId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "OtherUserId",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "ThisUsersId",
                table: "Chats",
                newName: "ParticipantIds");
        }
    }
}
