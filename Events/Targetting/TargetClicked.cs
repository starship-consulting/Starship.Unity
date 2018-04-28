using Assets.Scripts.Interaction;

namespace Assets.Scripts.Events.Targetting {
    public class TargetClicked {
        public TargetClicked() {
        }

        public TargetClicked(Targettable target) {
            Target = target;
        }

        public Targettable Target { get; set; }
    }
}