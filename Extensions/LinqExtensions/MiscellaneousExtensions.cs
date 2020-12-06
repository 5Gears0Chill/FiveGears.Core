using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FiveGears.Core.Extensions.LinqExtensions
{
    public static class MiscellaneousExtensions
    {
        public static IEnumerable<TSource> Between<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector,TResult lowest, TResult highest) where TResult : IComparable<TResult>
        {
            return source.OrderBy(selector).
                SkipWhile(s => selector.Invoke(s).CompareTo(lowest) < 0).
                TakeWhile(s => selector.Invoke(s).CompareTo(highest) <= 0);
        }

        public static bool IsBetween<T>(this T @this, T aInclusive, T bInclusive) where T : IComparable<T>
        {
            return @this.CompareTo(aInclusive) <= 0 && @this.CompareTo(bInclusive) >= 0
            || @this.CompareTo(aInclusive) >= 0 && @this.CompareTo(bInclusive) <= 0;
        }
    }
}
