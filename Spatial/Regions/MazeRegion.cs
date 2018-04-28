using System;

namespace Assets.Scripts.Spatial.Regions {
    public class MazeZone : Region {
        public MazeZone(int width, int height) : base(width, height) {
        }

        public override Tile[,] GetTiles(GeneratorContext context) {
            throw new NotImplementedException();
        }
    }
}