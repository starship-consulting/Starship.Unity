using Starship.Unity.Interaction;

namespace Starship.Unity.Events.Targetting {
    public class TargetChanged {
        public TargetChanged() {
        }

        public TargetChanged(Targettable target) {
            Target = target;
        }

        public Targettable Target { get; set; }
    }
}