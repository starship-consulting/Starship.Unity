using Assets.Scripts.Attributes;
using Assets.Scripts.Core;
using Assets.Scripts.Interaction;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Utility {
    public class DestroyComponent : BaseComponent, RequiresTarget {

        protected override void Start() {
            base.Start();

            if (Type == null) {
                return;
            }

            var component = GetComponent(Type.GetSerializedType());

            if (component != null) {
                Destroy(component);
            }

            Destroy(this);
        }

        public bool IsValidTarget(Targettable target) {
            if (Type == null) {
                return true;
            }

            return target.GetComponent(Type.GetSerializedType()) != null;
        }

        [ValidTypes(typeof(BaseComponent))]
        public SerializableType Type;
    }
}