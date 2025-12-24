namespace Finnova.Domain.Entities
{
    public class User
    {   
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; } 
        public DateTime UpdatedAt { get; private set; }

        private User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public static User Create(string name, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "name cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@') )
            {
                throw new ArgumentException("invalid email format", nameof(email));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password), "password is required");
            }

            return new User(name, email, password);
        }

        public void Update(string name, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "name cannot be empty");
            }
            
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            {
                throw new ArgumentException("invalid email format", nameof(email));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("password hash is required.");
            }

            Name = name;
            Email = email;
            Password = password;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}