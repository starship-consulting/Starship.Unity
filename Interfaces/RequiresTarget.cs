using Starship.Unity.Interaction;

namespace Starship.Unity.Interfaces {
    public interface RequiresTarget {
        bool IsValidTarget(Targettable target);
    }
}