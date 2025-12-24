namespace Finnova.Domain.Entities
{
    public class VerificationCode
    {
        public string Email { get; private set; }
        public string Code { get; private set; }
        public DateTime Expiration { get; private set; }

        private VerificationCode(string email, string code)
        {
            Email = email;
            Code = code;
            Expiration = DateTime.UtcNow.AddMinutes(5);
        }

        public static VerificationCode Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            {
                throw new ArgumentException("invalid email format", nameof(email));
            }

            return new VerificationCode(email, new Random().Next(100000, 999999).ToString());
        }

        public void Validate(string code)
        {
            if (Expiration < DateTime.UtcNow)
                throw new InvalidOperationException("verification code has expired");

            if (Code != code)
                throw new UnauthorizedAccessException("invalid verification code");
        }
    }
}