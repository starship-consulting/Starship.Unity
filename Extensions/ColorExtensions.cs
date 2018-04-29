using System;
using UnityEngine;
using Random = System.Random;

namespace Starship.Unity.Extensions {
    public static class ColorExtensions {

        static ColorExtensions() {
            RandomSeed = new Random(DateTime.Now.Millisecond);
        }

        public static Color Random(this Color color) {
            color.r = (float)RandomSeed.NextDouble();
            color.g = (float)RandomSeed.NextDouble();
            color.b = (float)RandomSeed.NextDouble();
            color.a = 1;
            return color;
        }

        private static Random RandomSeed { get; set; }
    }
}
