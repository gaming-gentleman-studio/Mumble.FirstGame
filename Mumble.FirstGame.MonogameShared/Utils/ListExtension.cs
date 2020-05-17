using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.Utils
{
    public static class ListExtension
    {
        public static void AddIfNotNull<T>(this List<T> list, T item)
        {
            if (item != null)
            {
                list.Add(item);
            }
        }
    }
}
