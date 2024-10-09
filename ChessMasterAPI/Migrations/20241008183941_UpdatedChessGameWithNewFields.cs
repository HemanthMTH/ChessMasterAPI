using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessMasterAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedChessGameWithNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WhitePlayerName",
                table: "ChessGames",
                newName: "PlayerNameWhite");

            migrationBuilder.RenameColumn(
                name: "BlackPlayerName",
                table: "ChessGames",
                newName: "PlayerNameBlack");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GameDate",
                table: "ChessGames",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "BlackElo",
                table: "ChessGames",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ECO",
                table: "ChessGames",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "ChessGames",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "ChessGames",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Site",
                table: "ChessGames",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Termination",
                table: "ChessGames",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeControl",
                table: "ChessGames",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhiteElo",
                table: "ChessGames",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlackElo",
                table: "ChessGames");

            migrationBuilder.DropColumn(
                name: "ECO",
                table: "ChessGames");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "ChessGames");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "ChessGames");

            migrationBuilder.DropColumn(
                name: "Site",
                table: "ChessGames");

            migrationBuilder.DropColumn(
                name: "Termination",
                table: "ChessGames");

            migrationBuilder.DropColumn(
                name: "TimeControl",
                table: "ChessGames");

            migrationBuilder.DropColumn(
                name: "WhiteElo",
                table: "ChessGames");

            migrationBuilder.RenameColumn(
                name: "PlayerNameWhite",
                table: "ChessGames",
                newName: "WhitePlayerName");

            migrationBuilder.RenameColumn(
                name: "PlayerNameBlack",
                table: "ChessGames",
                newName: "BlackPlayerName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GameDate",
                table: "ChessGames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
