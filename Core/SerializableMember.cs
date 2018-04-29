using System;
using System.Linq;
using System.Reflection;
using Starship.Unity.Enumerations;
using Starship.Unity.Extensions;
using UnityEngine;

namespace Starship.Unity.Core {

    [Serializable]
    public class SerializableMember {

        public void SetValue(object value) {
            var member = GetMember();

            if (member != null) {
                var component = GetComponent();

                if (component == null) {
                    return;
                }

                member.Set(component, value);
            }
        }

        public MemberInfo GetMember() {
            if (Source == null || MemberName.IsEmpty()) {
                return null;
            }

            var component = GetComponent();

            if (component != null) {
                return component.GetType().GetMember(MemberName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).FirstOrDefault();
            }

            return Source.GetType().GetMember(MemberName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).FirstOrDefault();
        }

        public static Component Get(MonoBehaviour behaviour, string typeName, string assignedIndex) {
            return Get(behaviour.gameObject, typeName, assignedIndex);
        }

        public static Component Get(GameObject obj, string typeName, string assignedIndex) {
            if (string.IsNullOrEmpty(typeName)) {
                return null;
            }

            var type = Type.GetType(typeName.Split('|').First());

            if (type == null) {
                return null;
            }

            var index = 0;

            if (!string.IsNullOrEmpty(assignedIndex)) {
                index = int.Parse(assignedIndex);
            }

            return obj.GetComponents(type).Skip(index).FirstOrDefault();
        }

        public Component Get(MonoBehaviour behaviour) {
            return Get(behaviour, ComponentName, Index);
        }

        public Component GetComponent() {
            if (Source == null) {
                return null;
            }
            
            if (Source is Component) {
                return (Component)Source;
            }

            if (ComponentName.IsEmpty()) {
                return null;
            }

            return Get((GameObject)Source, ComponentName, Index);
        }

        public EventInfo GetEvent() {
            if (Source != null) {
                var component = GetComponent();

                if (component != null) {
                    return component.GetType().GetEvent(MemberName);
                }
            }

            return null;
        }

        [HideInInspector]
        public UnityEngine.Object Source;

        [HideInInspector]
        public string ComponentName;

        [HideInInspector]
        public string Index;

        [HideInInspector]
        public string MemberName;

        [HideInInspector]
        public SerializableMemberTypes MemberType;
    }
}