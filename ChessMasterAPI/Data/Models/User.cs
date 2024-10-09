using Microsoft.AspNetCore.Identity;

namespace ChessMasterAPI.Data.Models
{
    public class User: IdentityUser
    {
        public string FullName { get; set; }
    }
}
