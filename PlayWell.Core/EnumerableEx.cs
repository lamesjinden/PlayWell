﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayWell.Core
{

    public static class EnumerableEx
    {

        /// <summary>
        /// Places <paramref name="value"/> into an IEnumerable of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<T> Lift<T>(this T value)
        {
            return new T[] {value};
        }

        /// <summary>
        /// Returns <paramref name="source"/> or, if null, Enumerable.Empty of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// Returns <paramref name="source"/> or, if null, a new empty List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<T> OrEmpty<T>(this List<T> source)
        {
            return source ?? new List<T>();
        }

        /// <summary>
        /// Returns <paramref name="source"/> or, if null, a new empty array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T[] OrEmpty<T>(this T[] source)
        {
            return source ?? new T[0];
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

        /// <summary>
        /// Checks whether <paramref name="source"/> is in <paramref name="items"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool In<T>(this T source, params T[] items)
        {
            return items.Contains(source);
        }

        /// <summary>
        /// Checks whether <paramref name="source"/> is in <paramref name="items"/>, using the provided <paramref name="comparer"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool In<T>(this T source, IEqualityComparer<T> comparer, params T[] items)
        {
            return items.Contains(source, comparer);
        }

        /// <summary>
        /// Checks whether <paramref name="source"/> is in <paramref name="items"/>. This operation is backed by a HashSet.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool InSet<T>(this T source, params T[] items)
        {
            return new HashSet<T>(items).Contains(source);
        }

        /// <summary>
        /// Checks whether <paramref name="source"/> is in <paramref name="items"/>, using the provided <paramref name="comparer"/>. This operation is backed by a HashSet.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool InSet<T>(this T source, IEqualityComparer<T> comparer, params T[] items)
        {
            return new HashSet<T>(comparer).Contains(source);
        }

    }

}
