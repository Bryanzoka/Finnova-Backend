using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BankAccountAPI.Services.Interface;
using MimeKit;

namespace BankAccountAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendVerificationCode(string toEmail, string validationCode)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Bank", _configuration["EmailSettings:From"]));
            emailMessage.To.Add(MailboxAddress.Parse(toEmail));
            emailMessage.Subject = "Email verification code";
            
            emailMessage.Body = new TextPart("plain")
            {
                Text = $"Your verification code is: {validationCode}"
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