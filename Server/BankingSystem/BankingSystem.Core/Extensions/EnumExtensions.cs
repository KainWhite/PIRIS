using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BankingSystem.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum e)
        {
            Type genericEnumType = e.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(e.ToString());
            if (memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attributes.Any())
                {
                    return ((DescriptionAttribute)attributes.ElementAt(0)).Description;
                }
            }
            return e.ToString();
        }
    }
}
