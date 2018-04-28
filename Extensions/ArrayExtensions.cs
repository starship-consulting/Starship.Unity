using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Extensions {
    public static class ArrayExtensions {

        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> sequence, Random random) {
            T[] retArray = sequence.ToArray();

            for (int i = 0; i < retArray.Length - 1; i += 1) {
                int swapIndex = random.Next(i, retArray.Length);
                if (swapIndex != i) {
                    T temp = retArray[i];
                    retArray[i] = retArray[swapIndex];
                    retArray[swapIndex] = temp;
                }
            }

            return retArray;
        }

        public static T[] Add<T>(this T[] array, T item) {
            var list = array.ToList();
            list.Add(item);
            return list.ToArray();
        }

        public static T[,] Fill<T>(this T[,] array, Func<T> func) {

            var width = array.GetLength(0);
            var height = array.GetLength(1);

            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    array[x, y] = func();
                }
            }

            return array;
        }

        public static IEnumerable<T> Select<T>(this T[,] array) {
            var width = array.GetLength(0);
            var height = array.GetLength(1);

            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    yield return array[x, y];
                }
            }
        }

        public static void Iterate<T>(this T[,] array, Action<int, int, T> action) {
            var width = array.GetLength(0);
            var height = array.GetLength(1);

            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    action(x, y, array[x, y]);
                }
            }
        }
    }
}
