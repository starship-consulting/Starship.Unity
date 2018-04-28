using System;
using Assets.Scripts.Core;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.World {
    
    public class WorldController : BaseComponent {

        public void AddToWorld(GameObject prefab) {
            this.Create(prefab);
        }
    }
}