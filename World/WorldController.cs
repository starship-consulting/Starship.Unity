using Starship.Unity.Core;
using Starship.Unity.Extensions;
using UnityEngine;

namespace Starship.Unity.World {
    
    public class WorldController : BaseComponent {

        public void AddToWorld(GameObject prefab) {
            this.Create(prefab);
        }
    }
}