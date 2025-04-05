using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollUsers_AspNetUsers_UserId",
                table: "PollUsers");

            migrationBuilder.DropIndex(
                name: "IX_PollUsers_UserId",
                table: "PollUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PollUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PollUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_PollUsers_UserId",
                table: "PollUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PollUsers_AspNetUsers_UserId",
                table: "PollUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
