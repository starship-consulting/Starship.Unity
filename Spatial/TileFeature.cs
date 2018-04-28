using System;
using UnityEngine;

namespace Assets.Scripts.Spatial {

    [Serializable]
    public class TileFeature {

        public virtual Color GetTileColor() {
            return Color.white;
        }
    }
}