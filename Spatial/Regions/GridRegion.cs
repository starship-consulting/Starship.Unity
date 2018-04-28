using System;

namespace Assets.Scripts.Spatial.Regions {
    public class GridRegion : Region {
        public GridRegion(int width, int height) : base(width, height) {
        }

        public override Tile[,] GetTiles(GeneratorContext context) {
            throw new NotImplementedException();
        }
    }
}