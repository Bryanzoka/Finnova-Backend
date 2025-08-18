using System.Net.Mail;

namespace FinnovaAPI.Helpers
{
    public static class ClientValidator
    {
        public static void ValiteId(int id)
        { 
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id");
            }
        }

        public static void ValidateCpf(string cpf)
        {
            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            {
                throw new ArgumentException("Invalid CPF");
            }
        }

        public static void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !MailAddress.TryCreate(email, out _))
            {
                throw new ArgumentException("Invalid email format");
            }
        }

        public static void ValidatePhone(string phone)
        { 
            if (phone.Length < 11 || phone.Length > 13 || !phone.All(char.IsDigit))
            {
                throw new ArgumentException("Phone number must contain only 11 to 13 digits");
            }
        }
    }
}