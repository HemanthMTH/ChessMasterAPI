﻿namespace ChessMasterAPI.Data.Models
{
    public class Register
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

}
