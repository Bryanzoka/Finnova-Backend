namespace Finnova.Domain.Aggregates
{
    public class Client
    {   
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; } 
        public DateTime UpdatedAt { get; private set; }

        private Client(string name, string cpf, string email, string phone, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Name cannot be empty");
                
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11 || !cpf.All(char.IsDigit))
                throw new ArgumentException("CPF must have exactly 11 digits and contain only numbers", nameof(cpf));

            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@') )
                throw new ArgumentException("Invalid email format", nameof(email));

            if (string.IsNullOrWhiteSpace(phone) || phone.Length < 11 || phone.Length > 13 || !phone.All(char.IsDigit))
                throw new ArgumentException("Phone must be between 11 and 13 digits and contain only numbers", nameof(phone));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), "Password is required");

            Name = name;
            Cpf = cpf;
            Email = email;
            Phone = phone;
            Password = password;
            CreatedAt = DateTime.Now;
        }

        public static Client Create(string name, string cpf, string email, string phone, string password)
        {
            return new Client(name, cpf, email, phone, password);
        }

        public void Update(string name, string email, string phone)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Name cannot be empty");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@') )
                throw new ArgumentException("Invalid email format", nameof(email));

            if (string.IsNullOrWhiteSpace(phone) || phone.Length < 11 || phone.Length > 13 || !phone.All(char.IsDigit))
                throw new ArgumentException("Phone must be between 11 and 13 digits and contain only numbers", nameof(phone));

            Name = name;
            Email = email;
            Phone = phone;
            UpdatedAt = DateTime.Now;
        }

        public void SetHashPassword(string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentException("Password hash is required.");

            Password = hashedPassword;
        }
    }
}