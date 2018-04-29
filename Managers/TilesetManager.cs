using System;
using Starship.Unity.Spatial;
using UnityEngine;

namespace Starship.Unity.Managers {

    [Serializable]
    public class TilesetManager : ScriptableObject {

        public TileFeature[] Definitions;
    }
}