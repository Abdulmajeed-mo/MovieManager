using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MJDVerse.Application.Interfaces;
using MJDVerse.Application.Options;
using System.Net;
using System.Net.Mail;

namespace MJDVerse.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpSettings _settings;
        private readonly IConfiguration _configuration;

        public EmailSender(
            IOptions<SmtpSettings> options,
            IConfiguration configuration)
        {
            _settings = options.Value;
            _configuration = configuration;
        }

        public async Task SendEmailAsync(
            string email,
            string subject,
            string body)
        {
            var password = _configuration["SmtpSettings:Password"];

            using var client = new SmtpClient(_settings.Host, _settings.Port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(
                    _settings.Username,
                    password)
            };

            using var message = new MailMessage
            {
                From = new MailAddress(_settings.Username),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            message.To.Add(email);

            
            await client.SendMailAsync(message);
        }
    }
}