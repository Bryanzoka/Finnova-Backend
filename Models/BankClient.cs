using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BankAccountAPI.Models.DTOs;

namespace BankAccountAPI.Models
{
    public class BankClientModel
    {
        [Key]
        [Required(ErrorMessage = "CPF is required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF must be 11 digits long")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF must contain only numbers")]
        public string CPF { get; private set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "The entered name is to long")]
        public string ClientName { get; private set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string ClientEmail { get; private set; }

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "Phone number must be between 11 and 13 digits long")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must contain only numbers")]
        public string ClientTel { get; private set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public BankClientModel() { }

        [JsonConstructor]
        public BankClientModel(string cpf, string clientName, string clientEmail, string clientTel, string password)
        {
            CPF = cpf;
            ClientName = clientName;
            ClientEmail = clientEmail;
            ClientTel = clientTel;
            Password = password;
            CreatedAt = DateTime.Now;
        }

        public void UpdateClient(string clientName, string clientEmail, string clientTel, string password)
        {
            ClientName = clientName;
            ClientEmail = clientEmail;
            ClientTel = clientTel;
            Password = password;
            UpdatedAt = DateTime.Now;
        }

        public static BankClientModel ToModel(BankClientDTO bankClientDTO)
        {
            return new BankClientModel
            {
                ClientName = bankClientDTO.ClientName,
                ClientEmail = bankClientDTO.ClientEmail,
                ClientTel = bankClientDTO.ClientTel
            };
        }
    }
}