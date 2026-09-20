using MJDVerse.Application.Interfaces;
using Resend;

namespace MJDVerse.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IResend _resend;

        public EmailSender(IResend resend)
        {
            _resend = resend;
        }

        public async Task SendEmailAsync(
            string email,
            string subject,
            string body)
        {
            var message = new EmailMessage
            {
                From = "onboarding@resend.dev",
                Subject = subject,
                TextBody = body
            };

            message.To.Add(email);

            await _resend.EmailSendAsync(message);
        }
    }
}