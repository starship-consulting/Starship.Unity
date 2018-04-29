using System.Collections.Generic;

namespace Starship.Unity.Spatial {
    public class Tile {

        public Tile(params TileFeature[] features) {
            Features = new List<TileFeature>(features);
        }

        public List<TileFeature> Features { get; set; } 
    }
}