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
        public static Tuple<TSource, int> MaxWithIndex<TSource>(IEnumerable<TSource> source, Func<TSource, decimal?> selector) 
        {
            return !source.Any() ? null :
                   source
                   .Select((value, index) => new Tuple<TSource, int>(value, index))
                   .Aggregate((a, b) => (selector(a.Item1) > selector(b.Item1)) ? a : b);
        }
    }
}
