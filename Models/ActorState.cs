using System;
using UnityEngine;

namespace Assets.Scripts.Models {
    
    [Serializable]
    public class ActorState {

        public string Name;

        public GameObject Mesh;
        
        public override string ToString() {
            return Name;
        }
    }
}