using System;
using Assets.Scripts.Spatial;
using UnityEngine;

namespace Assets.Scripts.Managers {

    [Serializable]
    public class TilesetManager : ScriptableObject {

        public TileFeature[] Definitions;
    }
}