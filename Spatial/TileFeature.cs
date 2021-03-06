﻿using System;
using UnityEngine;

namespace Starship.Unity.Spatial {

    [Serializable]
    public class TileFeature {

        public virtual Color GetTileColor() {
            return Color.white;
        }
    }
}