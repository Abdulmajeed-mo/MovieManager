using MJDVerse.Application.Interfaces;

namespace MJDVerse.Infrastructure.Services
{
    public class OtpRateLimiter : IOtpRateLimiter
    {
        private readonly Dictionary<string, List<DateTime>> _requests = new();

        public bool IsAllowed(string key)
        {
            var now = DateTime.UtcNow;

            if (!_requests.ContainsKey(key))
            {
                _requests[key] = new List<DateTime>();
            }

            _requests[key].RemoveAll(
                time => time < now.AddSeconds(-60));

            if (_requests[key].Count >= 4)
            {
                return false;
            }

            _requests[key].Add(now);

            return true;
        }
    }
}