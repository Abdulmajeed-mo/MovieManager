namespace MJDVerse.Application.Interfaces
{
    public interface IOtpRateLimiter
    {
        bool IsAllowed(string key);
    }
}