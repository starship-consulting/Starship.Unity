using System;

namespace Starship.Unity.Spatial {
    public class GeneratorContext {
        public GeneratorContext(int seed) {
            Random = new Random(seed);
        }

        public Random Random { get; set; }
    }
}
