using MJDVerse.Domain.Entities;

namespace MJDVerse.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser user);
    }
}