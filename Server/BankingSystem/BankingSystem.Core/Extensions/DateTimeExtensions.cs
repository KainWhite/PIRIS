using System;
using System.Collections.Generic;
using System.Text;

namespace BankingSystem.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetLastDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
        }

        public static bool IsLastDayOfMonth(this DateTime dateTime)
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month) == dateTime.Day;
        }
    }

}
