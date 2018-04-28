using Assets.Scripts.Interaction;

namespace Assets.Scripts.Events.Targetting {
    public class TargetChanged {
        public TargetChanged() {
        }

        public TargetChanged(Targettable target) {
            Target = target;
        }

        public Targettable Target { get; set; }
    }
}