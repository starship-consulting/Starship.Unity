using Assets.Scripts.Controls;
using UnityEngine;

namespace Assets.Scripts.Interaction {
    public class FollowCursor : MonoBehaviour {

        private void Awake() {
            IsUI = GetComponent<RectTransform>() != null;
        }

        private void Update() {

            if (IsUI) {
                transform.position = new Vector3(Input.mousePosition.x + Offset.x, Input.mousePosition.y + Offset.y, transform.position.z);
            }
            else {
                var raycast = MouseHelper.Raycast();
                var destination = raycast.point;
                destination.y = transform.position.y;

                transform.position = destination;
            }
        }

        public Vector2 Offset;

        private bool IsUI { get; set; }
    }
}
