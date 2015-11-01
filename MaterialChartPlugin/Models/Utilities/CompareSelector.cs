using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialChartPlugin.Models.Utilities
{
    /// <summary>
    /// 汎用のComparer（http://neue.cc/2009/08/07_184.html を参照）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class CompareSelector<T, TKey> : IEqualityComparer<T>
    {
        private Func<T, TKey> selector;

        public CompareSelector(Func<T, TKey> selector)
        {
            this.selector = selector;
        }

        public bool Equals(T x, T y)
        {
            return selector(x).Equals(selector(y));
        }

        public int GetHashCode(T obj)
        {
            return selector(obj).GetHashCode();
        }
    }
}
