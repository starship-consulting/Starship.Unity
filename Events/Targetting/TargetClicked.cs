using Starship.Unity.Interaction;

namespace Starship.Unity.Events.Targetting {
    public class TargetClicked {
        public TargetClicked() {
        }

        public TargetClicked(Targettable target) {
            Target = target;
        }

        public Targettable Target { get; set; }
    }
}