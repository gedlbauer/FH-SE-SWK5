using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement
{
    public static class CollectionExtensions
    {
        public static ICollection<T> AddRange<T>(this ICollection<T> list, IEnumerable<T> listToAdd)
        {
            foreach (var item in listToAdd)
            {
                list.Add(item);
            }
            return list; // originales AddRange hat Rückgabetyp void
        }
    }
}
