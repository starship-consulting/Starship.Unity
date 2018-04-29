using UnityEngine;

namespace Starship.Unity.Debugging {
    public class DrawDebugRay : MonoBehaviour {

        public void Update() {
            Debug.DrawRay(From, To, Color.magenta);
        }

        public Vector3 From;

        public Vector3 To;
    }
}