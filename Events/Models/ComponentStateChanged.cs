using System;
using Assets.Scripts.Core;
using Assets.Scripts.Enumerations;
using UnityEngine;

namespace Assets.Scripts.Events.Models {
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