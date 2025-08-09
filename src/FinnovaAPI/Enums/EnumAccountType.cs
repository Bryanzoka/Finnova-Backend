using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;


namespace FinnovaAPI.Enums
{
    public enum EnumAccountType
    {
        [Description("current")]
        Current,
        [Description("savings")]
        Savings
    }
}