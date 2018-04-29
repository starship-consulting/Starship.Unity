using Starship.Unity.Extensions;
using Starship.Unity.Spatial.Regions;
using Starship.Unity.Spatial.TileFeatures;

namespace Starship.Unity.Spatial {
    public class DungeonGenerator {
        
        public DungeonGenerator(int size, int seed) {
            Size = size;
            Context = new GeneratorContext(seed);
        }

        public Dungeon Generate() {
            var dungeon = new Dungeon(Size);

            X = Context.Random.Next(0, Size);
            Y = Context.Random.Next(0, Size);

            //AddRegion(dungeon, new ConnectionRegion(20, 20, new Coordinate(1, 1), new Coordinate(19, 19)));
            AddRegion(dungeon, new CircularRegion(20, 20));

            // Fill remaining with wall
            dungeon.Tiles.Iterate((x, y, tile) => {
                if (tile == null) {
                    dungeon.Tiles[x, y] = new Tile(new DungeonWallFeature());
                }
            });

            return dungeon;
        }

        private void AddRegion(Dungeon dungeon, Region region) {
            var tiles = region.GetTiles(Context);
            tiles.Iterate((x, y, tile) => dungeon.Tiles[x + (Size / 2), y + (Size / 2)] = tile);
        }

        private DungeonLayouts Layout { get; set; }

        private int X { get; set; }

        private int Y { get; set; }

        private int Size { get; set; }

        private GeneratorContext Context { get; set; }
    }
}
