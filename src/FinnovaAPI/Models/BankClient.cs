using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FinnovaAPI.Models.DTOs.Client;

namespace FinnovaAPI.Models
{
    public class BankClientModel
    {   
        [Key]
        public int Id { get; private set; }

        [Required(ErrorMessage = "CPF is required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF must be 11 digits long")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF must contain only numbers")]
        public string Cpf { get; private set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "The entered name is to long")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; private set; }

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "Phone number must be between 11 and 13 digits long")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must contain only numbers")]
        public string Phone { get; private set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; }

        public BankClientModel() { }

        [JsonConstructor]
        public BankClientModel(string cpf, string name, string email, string phone, string password)
        {
            Cpf = cpf;
            Name = name;
            Email = email;
            Phone = phone;
            Password = password;
        }

        public void UpdateClient(string name, string email, string phone, string password)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Password = password;
            UpdatedAt = DateTime.UtcNow;
        }

        public static BankClientModel ToModel(BankClientDTO bankClientDTO)
        {
            return new BankClientModel
            {
                Name = bankClientDTO.Name,
                Email = bankClientDTO.Email,
                Phone = bankClientDTO.Phone
            };
        }

        public static BankClientModel FromRegister(RegisterClientDTO client)
        {
            return new BankClientModel
            {
                Cpf = client.Cpf,
                Name = client.Name,
                Email = client.Email,
                Phone = client.Phone,
                Password = client.Password
            };
        }

        public void HashPassword(string hashedPassword)
        {
            Password = hashedPassword;
        }
    }
}