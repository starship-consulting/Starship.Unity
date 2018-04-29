using System;
using Starship.Unity.Core;
using Starship.Unity.Extensions;
using Starship.Unity.Spatial;
using Starship.Unity.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Starship.Unity.UI {
    public class MapController : BaseComponent {

        protected override void Start() {
            base.Start();

            GetComponent<RectTransform>().sizeDelta = new Vector2(Size * TileSize, Size * TileSize);
            this.GetOrAdd<GridLayoutGroup>().cellSize = new Vector2(TileSize, TileSize);

            InitializeTexture();
            Generate();
        }

        private void InitializeTexture() {
            Texture = new Texture2D(Size * TileSize, Size * TileSize);
            GetComponent<RawImage>().texture = Texture;
            Clear();
        }

        private void Generate() {
            using (new Benchmark("Dungeon generated.")) {

                var generator = new DungeonGenerator(Size, DateTime.Now.Millisecond);
                Dungeon = generator.Generate();

                for (var x = 0; x < Size; x++) {
                    for (var y = 0; y < Size; y++) {
                        var tile = Dungeon.Tiles[x, y];

                        if (tile == null) {
                            continue;
                        }

                        foreach (var feature in tile.Features) {
                            var cell = new Rect(x*TileSize, y*TileSize, TileSize, TileSize);
                            Texture.DrawRectangle(cell, feature.GetTileColor());
                        }
                    }
                }

                Texture.Apply(true);
            }
        }

        public void Regenerate() {
            Clear();
            Generate();
        }

        private void Clear() {
            Texture.Fill(Color.white);
        }

        public GameObject GridCellTemplate;

        public int Size = 50;

        public int TileSize = 8;

        private Dungeon Dungeon { get; set; }

        private Texture2D Texture { get; set; }
    }
}