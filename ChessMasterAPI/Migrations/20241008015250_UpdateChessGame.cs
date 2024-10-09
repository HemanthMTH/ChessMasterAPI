using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessMasterAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChessGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FEN",
                table: "ChessGames",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FEN",
                table: "ChessGames");
        }
    }
}
