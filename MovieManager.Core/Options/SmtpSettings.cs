namespace MJDVerse.Application.Options
{
    public class SmtpSettings
    {
        public string Host { get; set; } = string.Empty;

        public int Port { get; set; }

        public string Username { get; set; } = string.Empty;
    }
}