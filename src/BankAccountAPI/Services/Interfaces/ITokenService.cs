using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Models;

namespace BankAccountAPI.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(BankClientModel bankClient);
    }
}