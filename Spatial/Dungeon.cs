namespace Starship.Unity.Spatial {
    public class Dungeon {

        public Dungeon(int size) {
            Tiles = new Tile[size, size];

            //Tiles = new Tile[size, size].Fill(() => new Tile(new DungeonWallFeature()));
        }

        public Tile[,] Tiles { get; set; }
    }
}