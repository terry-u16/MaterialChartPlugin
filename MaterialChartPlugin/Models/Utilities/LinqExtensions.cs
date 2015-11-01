using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialChartPlugin.Models.Utilities
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            return source.Distinct(new CompareSelector<T, TKey>(selector));
        }

        public static IEnumerable<T> Union<T, TKey>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, TKey> selector)
        {
            return first.Union(second, new CompareSelector<T, TKey>(selector));
        }
    }
}
