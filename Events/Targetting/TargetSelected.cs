using Assets.Scripts.Interaction;

namespace Assets.Scripts.Events.Targetting {
    public class TargetSelected {
        public TargetSelected() {
        }

        public TargetSelected(Targettable target) {
            Target = target;
        }

        public Targettable Target { get; set; }
    }
}