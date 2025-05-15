using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Models;

namespace BankAccountAPI.Models.DTOs
{
    public class UpdateClientDTO
    {
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientTel { get; set; }
    }
}