using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Extensions {
    public static class IEnumerableExtensions {

        public static IEnumerable<KeyValuePair<T, T>> Pairs<T>(this IEnumerable<T> collection) {
            return collection.SelectMany((value, index) => collection.Skip(index + 1), (first, second) => new KeyValuePair<T, T>(first, second));
        }

        public static T GetRandom<T>(this T[] collection) {
            return collection[Random.Next(collection.Length)];
        }

        public static T GetRandom<T>(this List<T> collection) {
            return collection[Random.Next(collection.Count)];
        }

        private static readonly Random Random = new Random();
    }
}