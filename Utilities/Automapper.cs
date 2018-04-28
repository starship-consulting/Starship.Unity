using System;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Attributes;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.Utilities {
    public static class Automapper {

        public static void AutomapRequiredComponents(MonoBehaviour behavior) {
            var type = behavior.GetType();
            var requiredComponents = type.GetAttributes<RequireAttribute>().ToList();

            if (requiredComponents.Any()) {
                foreach (var required in requiredComponents) {
                    var member = type.FindMember(required.Type.Name);

                    if (member != null) {
                        var component = behavior.GetComponent(required.Type);

                        if (component == null) {
                            component = behavior.gameObject.AddComponent(required.Type);
                        }

                        member.Set(behavior, component);
                        
                        if (!member.IsPublic() || member.HasAttribute<HideInInspector>()) {
                            component.hideFlags = HideFlags.HideInInspector;
                        }
                        else {
                            component.hideFlags = HideFlags.None;
                        }
                    }
                }
            }
        }
    }
}