using Assets.Scripts.Interaction;

namespace Assets.Scripts.Interfaces {
    public interface RequiresTarget {
        bool IsValidTarget(Targettable target);
    }
}