using Microsoft.AspNetCore.Identity;

namespace ChessMasterAPI.Data.Models
{
    public class User: IdentityUser
    {
        public required string FullName { get; set; }
    }
}
