using System;
using UnityEngine;

namespace Assets.Scripts.Events {
    public static class ComponentEvents<T> where T : MonoBehaviour {

        public static void Start(T component) {
            if (Started != null) {
                Started(component);
            }
        }

        public static void Enable(T component) {
            if (Enabled != null) {
                Enabled(component);
            }
        }

        public static void Disable(T component) {
            if (Disabled != null) {
                Disabled(component);
            }
        }

        public static event Action<T> Enabled;

        public static event Action<T> Started;

        public static event Action<T> Disabled;
    }
}