using Starship.Unity.Core;

namespace Starship.Unity.Components {
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