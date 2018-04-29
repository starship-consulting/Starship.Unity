using Starship.Unity.Interaction;

namespace Starship.Unity.Events.Targetting {
    public class TargetSelected {
        public TargetSelected() {
        }

        public TargetSelected(Targettable target) {
            Target = target;
        }

        public Targettable Target { get; set; }
    }
}