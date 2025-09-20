namespace Finnova.Domain.Aggregates
{
    public class VerificationCode
    {
        public string Email { get; private set; }
        public string Code { get; private set; }
        public DateTime Expiration { get; private set; }

        private VerificationCode(string email, string code)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                throw new ArgumentException("Invalid email format", nameof(email));

            if (string.IsNullOrWhiteSpace(code) || code.Length != 6 || !code.All(char.IsDigit))
                throw new ArgumentException("Invalid code format");

            Email = email;
            Code = code;
            Expiration = DateTime.Now.AddMinutes(5);
        }

        public static VerificationCode Create(string email)
        {
            var code = new Random().Next(100000, 999999).ToString();
            return new VerificationCode(email, code);
        }

        public void Validate(string code)
        {
            if (Expiration < DateTime.Now)
                throw new InvalidOperationException("Verification code has expired");

            if (Code != code)
                throw new UnauthorizedAccessException("Invalid verification code");
        }
    }
}