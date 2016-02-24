using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimogir.Services
{
    public static class Utilities
    {
        public static Tuple<TSource, int> MaxWithIndex<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Comparer<TSource> comparer = Comparer<TSource>.Default;

            return MaxWithIndex<TSource>(source, comparer.Compare);
        }

        public static Tuple<TSource, int> MaxWithIndex<TSource>(this IEnumerable<TSource> source, Comparison<TSource> comparison) 
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (comparison == null)
                throw new ArgumentNullException(nameof(comparison));

            var result = source
                         .DefaultIfEmpty()
                         .Select((value, index) => new { Value = value, Index = index })
                         .Aggregate((a, b) => comparison(a.Value, b.Value) >= 0 ? a : b);

            return result == null ? null : 
                                    Tuple.Create(result.Value, result.Index);
        }
    }
}
