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
        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve conter 11 dígitos")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF só deve conter números")]
        public string CPF { get; private set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome inserido é muito longo")]
        public string ClientName { get; private set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string ClientEmail { get; private set; }

        [Required(ErrorMessage = "Número de telefone é obrigatório")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "O número de telefone deve conter entre 11 a 13 dígitos")]
        [RegularExpression(@"^\d+$", ErrorMessage = "O número de telefone deve conter apenas números")]
        public string ClientTel { get; private set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(8, ErrorMessage = "A senha deve conter no mínimo 8 caracteres")]
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public BankClientModel() {}

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

        public void UpdateClient(string clientName, string clientEmail, string clientTel, DateTime updateDateTime)
        {
            ClientName = clientName;
            ClientEmail = clientEmail;
            ClientTel = clientTel;
            UpdatedAt = updateDateTime;
        }

        public static BankClientModel ToModelUpdate(BankClientDTO updateClientDTO)
        {
            return new BankClientModel
            {
                ClientName = updateClientDTO.ClientName,
                ClientEmail = updateClientDTO.ClientEmail,
                ClientTel = updateClientDTO.ClientTel
            };
        }
    }
}