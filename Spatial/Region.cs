namespace Assets.Scripts.Spatial {
    public abstract class Region {

        protected Region(int width, int height) {
            Width = width;
            Height = height;
        }

        public abstract Tile[,] GetTiles(GeneratorContext context);

        protected int Width { get; set; }

        protected int Height { get; set; }
    }
}