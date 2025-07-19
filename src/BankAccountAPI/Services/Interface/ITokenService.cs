using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Models;

namespace BankAccountAPI.Services.Interface
{
    public interface ITokenService
    {
        string GenerateToken(BankClientModel bankClient);
    }
}