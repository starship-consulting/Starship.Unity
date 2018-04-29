using System.Linq;
using Starship.Unity.Core;
using Starship.Unity.Extensions;
using Starship.Unity.Interaction;
using Starship.Unity.Interfaces;
using UnityEngine;

namespace Starship.Unity.Elements {
    public class Element : BaseComponent {
        
        public virtual GameObject CreateTemplate() {
            return Template != null ? Instantiate(Template) : new GameObject(Title);
        }

        public virtual void ApplyTo(GameObject instance) {
            foreach (var component in GetComponents<Component>()) {
                if (component is Element || component is Transform) {
                    continue;
                }

                var behaviour = component as MonoBehaviour;

                if (behaviour != null && !behaviour.enabled) {
                    continue;
                }

                var clone = instance.AddComponent(component.GetType());
                component.CopyTo(clone);
            }
        }

        public bool IsValidTarget(Targettable target) {
            var requirements = GetComponents<RequiresTarget>();

            if (!requirements.Any()) {
                return true;
            }

            if (target == null) {
                return false;
            }

            foreach (var component in requirements) {
                if (component.IsValidTarget(target)) {
                    return true;
                }
            }

            return false;
        }

        public string Title;

        public string Description;

        public Sprite Graphic;

        public Sprite Icon;
        
        public GameObject Template;

        public float PreviewOutline = 0.05f;

        public float PreviewDistance = 1f;
    }
}