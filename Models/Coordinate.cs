using System;
using System.Collections.Generic;
using UnityEngine;

namespace Starship.Unity.Models {
    public class Coordinate {

        public Coordinate() {
        }

        public Coordinate(int x, int y) {
            X = x;
            Y = y;
        }

        public Coordinate(Vector2 vector) {
            X = (int) vector.x;
            Y = (int) vector.y;
        }

        public List<Coordinate> GetAdjacent() {
            return new List<Coordinate> { North, East, South, West };
        }

        public override bool Equals(object target) {
            var coordinate = target as Coordinate;

            if (coordinate == null) {
                return false;
            }

            if (X == coordinate.X && Y == coordinate.Y) {
                return true;
            }

            return base.Equals(target);
        }

        public override int GetHashCode() {
            return (Y*10000) + X;
        }

        public int DistanceTo(Coordinate target) {
            return Math.Abs(X - target.X) + Math.Abs(Y - target.Y);
            //return Vector2.Distance(ToVector(), target.ToVector());

            //var distance = Vector2.Distance(ToVector(), target.ToVector());
            //return (int)Math.Round(distance, 0, MidpointRounding.AwayFromZero);
        }

        public Vector2 ToVector() {
            return new Vector2(X, Y);
        }

        public Coordinate Move(int direction) {
            switch (direction) {
                case 0:
                    return North;
                case 1:
                    return East;
                case 2:
                    return South;
                default:
                    return West;
            }
        }

        public Coordinate North { get { return new Coordinate(X, Y + 1); } }

        public Coordinate East { get { return new Coordinate(X + 1, Y); } }

        public Coordinate South { get { return new Coordinate(X, Y - 1); } }

        public Coordinate West { get {  return new Coordinate(X - 1, Y); } }

        public readonly int X;

        public readonly int Y;
    }
}
