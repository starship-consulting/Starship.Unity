using Starship.Unity.Extensions;
using UnityEngine;

namespace Starship.Unity.Utility {
    public class Delete : MonoBehaviour {

        public void Start() {
            if (Parents) {
                gameObject.DeleteParents();
            }
            else if (Object != null) {
                Object.Delete();
            }
            else if (Behaviour != null) {
                Behaviour.Destroy();
            }
        }

        public bool Parents = false;

        public GameObject Object;

        public MonoBehaviour Behaviour;
    }
}