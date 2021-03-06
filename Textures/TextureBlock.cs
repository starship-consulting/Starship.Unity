﻿using UnityEngine;

namespace Starship.Unity.Textures {

    public struct TextureBlock {

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Texture2D Texture { get; set; }
    }
}
