﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.James
{

    public static class EnumerableEx
    {

        /// <summary>
        /// Returns <paramref name="source"/> or, if null, Enumerable.Empty of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T> Maybe<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// Invokes <paramref name="action"/> on each item of <paramref name="source"/>
        /// and returns the item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> Tap<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
                yield return item;
            }
        }

        /// <summary>
        /// Filters <paramref name="source"/>, including only items that pass 
        /// every predicate in <paramref name="predicates"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhereAll<T>(this IEnumerable<T> source, params Func<T, bool>[] predicates)
        {
            foreach (var item in source)
            {
                if (predicates.All(p => p(item)))
                    yield return item;               
            }
        }

        /// <summary>
        /// Filters <paramref name="source"/>, including only items that pass
        /// at least one predicate in <paramref name="predicates"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhereAny<T>(this IEnumerable<T> source, params Func<T, bool>[] predicates)
        {
            foreach (var item in source)
            {
                if (predicates.Any(p => p(item)))
                    yield return item;
            }
        }

        /// <summary>
        /// Returns a IEnumerable of T that has been filtered for each 
        /// predicate in <paramref name="predicates"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhereMany<T>(this IEnumerable<T> source, params Func<T, bool>[] predicates)
        {
            return predicates.Aggregate(source, (seq, pred) => seq.Where(pred));
        }

    }

}
