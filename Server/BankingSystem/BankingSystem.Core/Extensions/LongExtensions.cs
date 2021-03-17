using System;
using System.Collections.Generic;
using System.Text;

namespace BankingSystem.Core.Extensions
{
    public static class LongExtensions
    {
        public static long GetDigitSum(this long x)
        {
            long result = 0;
            while (x > 0)
            {
                result += x % 10;
                x /= 10;
            }

            return result;
        }
    }
}
