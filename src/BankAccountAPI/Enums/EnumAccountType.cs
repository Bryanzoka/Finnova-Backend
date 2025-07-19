using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;


namespace BankAccountAPI.Enums
{
    public enum EnumAccountType
    {
        [Description("Corrente")]
        Current,
        [Description("Poupança")]
        Savings
    }
}