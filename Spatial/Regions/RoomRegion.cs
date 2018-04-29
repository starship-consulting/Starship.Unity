using Starship.Unity.Spatial.TileFeatures;

namespace Starship.Unity.Spatial.Regions {
    public class RoomRegion : Region {

        public RoomRegion(int width, int height)
            : base(width, height) {
        }

        public override Tile[,] GetTiles(GeneratorContext context) {
            var tiles = new Tile[Width, Height];

            // Add walls around the perimeter of the room
            for (var x = 0; x < Width; x++) {
                for (var y = 0; y < Height; y++) {

                    var tile = new Tile();

                    if (x == 0 || y == 0 || x == Width-1 || y == Height-1) {
                        tile.Features.Add(new DungeonWallFeature());
                    }

                    tiles[x, y] = tile;
                }
            }

            return tiles;
        }
    }
}