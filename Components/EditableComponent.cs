using System;
using Assets.Scripts.Core;

namespace Assets.Scripts.Components {
    public abstract class EditableComponent : BaseComponent {

        protected override void Start() {
            base.Start();
            UpdateState();
        }
        
        protected void ApplyChanges() {
            UpdateState();
        }

        protected abstract void UpdateState();
    }
}