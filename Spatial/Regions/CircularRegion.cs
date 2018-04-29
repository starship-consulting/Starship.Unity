namespace Starship.Unity.Spatial.Regions {

    public class CircularRegion : Region {

        public CircularRegion(int width, int height, bool isHollow = true) : base(width, height) {
            IsHollow = isHollow;
        }

        public override Tile[,] GetTiles(GeneratorContext context) {
            if (IsHollow) {
                return BruteForceAlgorithm();
            }

            return MidpointAlgorithm();
        }

        private Tile[,] BruteForceAlgorithm() {
            var tiles = new Tile[Width, Height];

            var originX = (Width/2);
            var originY = (Height/2);
            var radius = originX - 1;

            for (int y = -radius; y <= radius; y++) {
                for (int x = -radius; x <= radius; x++) {
                    if (x * x + y * y <= radius * radius + radius * 0.8f) {
                        tiles[originX + x, originY + y] = new Tile();
                    }
                }
            }

            return tiles;
        }

        private Tile[,] MidpointAlgorithm() {
            var tiles = new Tile[Width, Height];

            var x0 = (Width / 2) - 1;
            var y0 = (Height / 2) - 1;

            int x = x0;
            int y = 0;
            int decisionOver2 = 1 - x;

            while (x >= y) {
                tiles[x + x0, y + y0] = new Tile();
                tiles[y + x0, x + y0] = new Tile();
                tiles[-x + x0, y + y0] = new Tile();
                tiles[-y + x0, x + y0] = new Tile();
                tiles[-x + x0, -y + y0] = new Tile();
                tiles[-y + x0, -x + y0] = new Tile();
                tiles[x + x0, -y + y0] = new Tile();
                tiles[y + x0, -x + y0] = new Tile();

                y++;

                if (decisionOver2 <= 0) {
                    decisionOver2 += 2 * y + 1;
                }
                else {
                    x--;
                    decisionOver2 += 2 * (y - x) + 1;
                }
            }

            return tiles;
        }

        public bool IsHollow { get; set; }
    }
}
