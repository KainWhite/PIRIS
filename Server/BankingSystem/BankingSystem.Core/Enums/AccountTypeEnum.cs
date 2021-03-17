using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BankingSystem.Core.Enums
{
    public enum AccountTypeEnum
    {
        [Description("Cashier")]
        Cashier = 1,

        [Description("Bank development fund")]
        BankDevelopmentFund = 2,

        [Description("Current account")]
        CurrentAccount = 3,

        [Description("Percent account")]
        PercentAccount = 4
    }
}
