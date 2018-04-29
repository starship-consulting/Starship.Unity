using Starship.Unity.Core;
using Starship.Unity.Enumerations;
using UnityEngine;

namespace Starship.Unity.Events.Models {
    public class ComponentStateChanged {

        public ComponentStateChanged() {
        }

        public ComponentStateChanged(Behaviour component, ComponentStates state) {
            Component = component;
            State = state;
        }

        public T GetComponent<T>() where T : BaseComponent {
            return (T)Component;
        }

        public Behaviour Component { get; set; }

        public ComponentStates State { get; set; }
    }
}