using System;

namespace Assets.Scripts.Spatial {
    public class GeneratorContext {
        public GeneratorContext(int seed) {
            Random = new Random(seed);
        }

        public Random Random { get; set; }
    }
}
