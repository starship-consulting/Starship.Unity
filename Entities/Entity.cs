using System;
using Assets.Scripts.Core;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.Entities {
    public class Entity : BaseComponent {
        
        protected override void Awake() {
            foreach(var flag in Template.Flags) {
                foreach(var component in flag.Components) {
                    gameObject.CopyComponentsFrom(component.gameObject);
                }
            }
        }

        public Template Template;
    }
}