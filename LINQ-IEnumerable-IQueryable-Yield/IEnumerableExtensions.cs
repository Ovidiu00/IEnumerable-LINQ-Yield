using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_IEnumerable_IQueryable_Yield
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {

            foreach (var item in items)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }


        public static IEnumerable<TResult> MySelect<T, TResult>(this IEnumerable<T> items, Func<T, TResult> selector)
        {

            foreach (var item in items)
            {
                yield return selector(item);
            }
        }

        public static IEnumerable<TResult> MySelectMany<T, TResult>(this IEnumerable<T> items, Func<T, IEnumerable<TResult>> selector)
        {

            foreach (var item in items)
            {
                foreach(var innerItem in selector(item))
                {
                    yield return innerItem;

                }
            }
        }
        public static IEnumerable<TResult> NewJoin<T, TH, TKey, TResult>(
            this IEnumerable<T> items,
            IEnumerable<TH> innerItems,
            Func<T, TKey> outerKeySelector,
            Func<TH, TKey> innerKeySelector,
            Func<T, TH, TResult> resultSelector
            )
        {
            foreach (var item in items)
            {
                foreach (var innerItem in innerItems)
                {
                    if (outerKeySelector(item).Equals(innerKeySelector(innerItem)))
                    {
                        yield return resultSelector(item, innerItem);
                    }
                }
            }
        }
        public static IEnumerable<TItem> MyTake<TItem>(this IEnumerable<TItem> items, int count)
        {
            using (IEnumerator<TItem> enumerator = items.GetEnumerator())
                for (int index = 0; index < count && enumerator.MoveNext(); index++)
                    yield return enumerator.Current;
        }

    }
        
}
