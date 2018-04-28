using System.Collections.Generic;

namespace Assets.Scripts.Spatial {
    public class Tile {

        public Tile(params TileFeature[] features) {
            Features = new List<TileFeature>(features);
        }

        public List<TileFeature> Features { get; set; } 
    }
}