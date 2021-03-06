﻿using Starship.Unity.Attributes;
using Starship.Unity.Core;
using Starship.Unity.Interaction;
using Starship.Unity.Interfaces;

namespace Starship.Unity.Utility {
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