using UnityEngine;

namespace Starship.Unity.Controls {
    public static class MouseHelper {
        public static RaycastHit Raycast() {
            var position = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit target;
            Physics.Raycast(position, out target);
            return target;
        }

        public static Vector3 GetWorldPosition() {
            var position = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit target;

            if (Physics.Raycast(position, out target)) {
                return target.point;
            }

            return Vector3.zero;
        }
    }
}