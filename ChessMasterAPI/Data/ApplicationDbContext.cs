using ChessMasterAPI.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChessMasterAPI.Data
{
    public class ApplicationDbContext: IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options) { }
        
        public DbSet<ChessGame> ChessGames { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is ChessGame chessGame)
                {
                    // Ensure all DateTimes are UTC before saving
                    if (chessGame.GameDate.HasValue)
                    {
                        chessGame.GameDate = DateTime.SpecifyKind(chessGame.GameDate.Value, DateTimeKind.Utc);
                    }
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

    }
}
