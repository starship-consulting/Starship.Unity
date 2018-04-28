using UnityEngine;

namespace Assets.Scripts.Debugging {
    public class DrawDebugRay : MonoBehaviour {

        public void Update() {
            Debug.DrawRay(From, To, Color.magenta);
        }

        public Vector3 From;

        public Vector3 To;
    }
}