using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Models;

namespace BankAccountAPI.Models.DTOs.Client
{
    public record BankClientDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public static BankClientDTO ToDTO(BankClientModel model)
        {
            return new BankClientDTO
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone
            };
        }
    }
}