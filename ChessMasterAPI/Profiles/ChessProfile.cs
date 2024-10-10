using AutoMapper;
using ChessMasterAPI.DTOs;
using ChessMasterAPI.Data.Models;

namespace ChessMasterAPI.Profiles
{
    public class ChessProfile: Profile
    {
        public ChessProfile()
        {
            CreateMap<ChessGame, ChessGameDTO>().ReverseMap();  // Bidirectional mapping
            CreateMap<RegisterDTO, User>();  // Map RegisterDto to User
        }
    }
}
