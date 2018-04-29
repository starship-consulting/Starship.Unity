using System;
using System.Collections.Generic;
using System.Linq;
using Starship.Unity.Extensions;
using Starship.Unity.Models;
using Starship.Unity.Spatial.TileFeatures;

namespace Starship.Unity.Spatial.Regions {

    /// <summary>
    /// Creates a path between all entrances
    /// </summary>
    public class ConnectionRegion : Region {

        public ConnectionRegion(int width, int height, Coordinate from, Coordinate to, float complexity = 0) : base(width, height) {
            Complexity = complexity;
            From = from;
            To = to;
        }

        public override Tile[,] GetTiles(GeneratorContext context) {

            Tiles = new Tile[Width, Height];

            MaxDistance = (int)Math.Round(From.DistanceTo(To) * (Complexity + 1));

            UnityEngine.Debug.Log(MaxDistance);

            if (!FindPath(context, From, To)) {
                UnityEngine.Debug.Log("Unable to complete ConnectionRegion.");
            }

            // Fill in the rest with walls
            Tiles.Iterate((x, y, tile) => {
                if (tile == null) {
                    Tiles[x, y] = new Tile(new DungeonWallFeature());
                }
            });

            return Tiles;
        }

        private bool FindPath(GeneratorContext context, Coordinate from, Coordinate to) {

            if (from.Equals(to)) {
                Tiles[from.X, from.Y] = new Tile();
                return true;
            }

            var directions = new List<int> { 0, 1, 2, 3 }.Randomize(context.Random);

            MaxDistance -= 1;
            Tiles[from.X, from.Y] = new Tile();

            foreach (var direction in directions) {

                var node = from.Move(direction);

                if (IsOutOfBounds(node) || IsOccupied(node) || IsTooFar(node, to) || (!AllowAdjacent && HasAdjacent(node, from))) {
                    continue;
                }

                if (FindPath(context, node, to)) {
                    return true;
                }
            }

            MaxDistance += 1;
            Tiles[from.X, from.Y] = null;
            return false;
        }

        private bool HasAdjacent(Coordinate from, Coordinate ignore) {
            return from.GetAdjacent().Any(each => !each.Equals(ignore) && !IsOutOfBounds(each) && IsOccupied(each));
        }

        private bool IsTooFar(Coordinate from, Coordinate to) {
            return from.DistanceTo(to) > MaxDistance;
        }

        private bool IsOccupied(Coordinate coordinate) {
            return Tiles[coordinate.X, coordinate.Y] != null;
        }

        private bool IsOutOfBounds(Coordinate coordinate) {
            return coordinate.X >= Width || coordinate.Y >= Height || coordinate.X < 0 || coordinate.Y < 0;
        }

        public float Complexity { get; set; }

        public bool AllowAdjacent { get; set; }

        private int MaxDistance { get; set; }

        private Tile[,] Tiles { get; set; }

        private Coordinate From { get; set; }

        private Coordinate To { get; set; }
    }
}
