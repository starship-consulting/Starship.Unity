using System;
using UnityEngine;

namespace Starship.Unity.Models {
    
    [Serializable]
    public class ActorState {

        public string Name;

        public GameObject Mesh;
        
        public override string ToString() {
            return Name;
        }
    }
}