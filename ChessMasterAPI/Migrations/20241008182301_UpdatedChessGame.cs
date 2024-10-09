using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessMasterAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedChessGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlayerName",
                table: "ChessGames",
                newName: "WhitePlayerName");

            migrationBuilder.AddColumn<string>(
                name: "BlackPlayerName",
                table: "ChessGames",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlackPlayerName",
                table: "ChessGames");

            migrationBuilder.RenameColumn(
                name: "WhitePlayerName",
                table: "ChessGames",
                newName: "PlayerName");
        }
    }
}
