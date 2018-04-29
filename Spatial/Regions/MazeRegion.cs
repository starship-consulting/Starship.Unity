using System;

namespace Starship.Unity.Spatial.Regions {
    public class MazeZone : Region {
        public MazeZone(int width, int height) : base(width, height) {
        }

        public override Tile[,] GetTiles(GeneratorContext context) {
            throw new NotImplementedException();
        }
    }
}