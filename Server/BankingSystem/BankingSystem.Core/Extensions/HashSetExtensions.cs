using System;
using System.Collections.Generic;
using System.Text;

namespace BankingSystem.Core.Extensions
{
    public static class HashSetExtensions
    {
        public static void AddRange<T>(this HashSet<T> set, ICollection<T> collection)
        {
            foreach (var item in collection)
            {
                set.Add(item);
            }
        }
    }
}
