using MailKit.Net.Smtp;
using FinnovaAPI.Services.Interfaces;
using MimeKit;

namespace FinnovaAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendVerificationCode(string toEmail, string verificationCode)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Bank", _configuration["EmailSettings:From"]));
            emailMessage.To.Add(MailboxAddress.Parse(toEmail));
            emailMessage.Subject = "Email verification code";
            
            emailMessage.Body = new TextPart("plain")
            {
                Text = $"Your verification code is: {verificationCode}"
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration["EmailSettings:Server"], int.Parse(_configuration["EmailSettings:Port"]), true);
                await client.AuthenticateAsync(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}