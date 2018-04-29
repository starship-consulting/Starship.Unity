using System.Collections.Generic;
using Starship.Unity.Textures;
using UnityEngine;

namespace Starship.Unity.Extensions {
    public static class TextureExtensions {

        public static IEnumerable<TextureBlock> IterateBlocks(this Texture2D texture, int width, int height) {

            var rows = texture.width/width;
            var columns = texture.height/height;

            for (var x = 0; x < rows; x++) {
                for (var y = 0; y < columns; y++) {
                    yield return new TextureBlock {
                        X = x * width,
                        Y = y * height,
                        Width = width,
                        Height = height,
                        Texture = texture
                    };
                }
            }
        }

        public static Texture2D Fill(this Texture2D texture, Color color) {
            var pixels = texture.GetPixels();

            for (var i = 0; i < pixels.Length; ++i) {
                pixels[i] = color;
            }

            texture.SetPixels(pixels);
            texture.Apply();

            return texture;
        }

        public static TextureBlock Fill(this TextureBlock block, Color color) {
            block.Texture.DrawRectangle(new Rect(block.X, block.Y, block.Width, block.Height), color);
            return block;
        }

        public static Texture2D DrawRectangle(this Texture2D texture, Rect rectangle, Color color) {
            var size = (int)rectangle.width * (int)rectangle.height;
            var colors = new Color[size];

            for (var i = 0; i < colors.Length; ++i) {
                colors[i] = color;
            }

            texture.SetPixels((int)rectangle.x, (int)rectangle.y, (int)rectangle.width, (int)rectangle.height, colors);

            return texture;
        }
    }
}
