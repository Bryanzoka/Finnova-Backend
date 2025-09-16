namespace FinnovaAPI.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendVerificationCode(string toEmail, string verificationCode);
    }
}