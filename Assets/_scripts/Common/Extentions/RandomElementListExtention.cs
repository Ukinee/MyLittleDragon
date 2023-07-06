using System;
using System.Collections.Generic;

namespace Common.Extentions
{
    public static partial class ListExtentions
    {
        public static T RandomElement<T>(this List<T> list)
        {
            Random random = new();
            
            return list[random.Next(list.Count)];
        }

        public static T RandomElement<T>(this List<T> list, Random random)
        {
            return list[random.Next(list.Count)];
        }
    }
}