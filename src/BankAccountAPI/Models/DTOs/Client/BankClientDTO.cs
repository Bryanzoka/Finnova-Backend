using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Models;

namespace BankAccountAPI.Models.DTOs.Client
{
    public record BankClientDTO
    {
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientTel { get; set; }

        public static BankClientDTO ToDTO(BankClientModel model)
        {
            return new BankClientDTO
            {
                ClientName = model.ClientName,
                ClientEmail = model.ClientEmail,
                ClientTel = model.ClientTel
            };
        }
    }
}