using System;
using System.Collections.Generic;

namespace Common.Extentions
{
    public static partial class ListExtentions
    {
        public static void Shuffle<T>(this List<T> list)
        {
            Random random = new();

            for (int i = 0; i < list.Count; i++)
            {
                int randomIndex = random.Next(list.Count);
                (list[randomIndex], list[i]) = (list[i], list[randomIndex]);
            }
        }
        
        public static void Shuffle<T>(this List<T> list, Random random)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int randomIndex = random.Next(list.Count);
                (list[randomIndex], list[i]) = (list[i], list[randomIndex]);
            }
        }
    }
}