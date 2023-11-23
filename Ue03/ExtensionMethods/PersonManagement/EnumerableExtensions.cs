using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static IEnumerable<T> FilterBy<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            foreach (var item in list)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<R> Map<T, R>(this IEnumerable<T> list, Func<T, R> trans)
        {
            foreach (var item in list)
            {
                yield return trans(item);
            }
        }

        public static T MaxBy<T>(this IEnumerable<T> list, Comparison<T> comp)
        {
            if (list.Count() == 0)
            {
                throw new ArgumentException("Max not defined for empty lists");
            }
            return list.Aggregate(list.First(), (x, y) => comp(x, y) > 0 ? x : y);
        }
    }
}
