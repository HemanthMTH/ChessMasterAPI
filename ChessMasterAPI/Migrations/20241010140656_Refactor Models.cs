using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessMasterAPI.Migrations
{
    /// <inheritdoc />
    public partial class RefactorModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FEN",
                table: "ChessGames");

            migrationBuilder.AlterColumn<string>(
                name: "Result",
                table: "ChessGames",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ChessGames",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ChessGames_UserId",
                table: "ChessGames",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChessGames_AspNetUsers_UserId",
                table: "ChessGames",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChessGames_AspNetUsers_UserId",
                table: "ChessGames");

            migrationBuilder.DropIndex(
                name: "IX_ChessGames_UserId",
                table: "ChessGames");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ChessGames");

            migrationBuilder.AlterColumn<string>(
                name: "Result",
                table: "ChessGames",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "FEN",
                table: "ChessGames",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
