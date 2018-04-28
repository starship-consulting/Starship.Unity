using UnityEngine;

namespace Assets.Scripts.Extensions {
    public static class ColliderExtensions {

        public static void CopyTo(this Collider collider, GameObject target) {

            var boxCollider = collider as BoxCollider;

            if (boxCollider != null) {
                var newCollider = target.AddComponent<BoxCollider>();
                newCollider.center = new Vector3(boxCollider.center.x, boxCollider.center.y, boxCollider.center.z);
                newCollider.size = boxCollider.size;
                newCollider.isTrigger = true;
                return;
            }

            var capsuleCollider = collider as CapsuleCollider;

            if (capsuleCollider != null) {
                var newCollider = target.AddComponent<CapsuleCollider>();
                newCollider.center = new Vector3(capsuleCollider.center.x, capsuleCollider.center.y, capsuleCollider.center.z);
                newCollider.radius = capsuleCollider.radius;
                newCollider.height = capsuleCollider.height;
                newCollider.direction = capsuleCollider.direction;
                newCollider.isTrigger = true;
                return;
            }

            var sphereCollider = collider as SphereCollider;

            if (sphereCollider != null) {
                var newCollider = target.AddComponent<SphereCollider>();
                newCollider.center = new Vector3(sphereCollider.center.x, sphereCollider.center.y, sphereCollider.center.z);
                newCollider.radius = sphereCollider.radius;
                newCollider.isTrigger = true;
                return;
            }
        }
    }
}
