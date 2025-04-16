using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAccountAPI.Models
{
    public class BankClientModel
    {
        [Key]
        [Required]
        public string CPF { get; private set; }
        public string ClientName { get; private set; }
        public string ClientEmail { get; private set; }
        public string ClientTel { get; private set; }

        public BankClientModel() {}

        [JsonConstructor]
        public BankClientModel (string cpf, string clientName, string clientEmail, string clientTel)
        {
            CPF = cpf;
            ClientName = clientName;
            ClientEmail = clientEmail;
            ClientTel = clientTel;
        }

        public void UpdateClient(string clientName, string clientEmail, string clientTel)
        {
            ClientName = clientName;
            ClientEmail = clientEmail;
            ClientTel = clientTel;
        }
    }
}