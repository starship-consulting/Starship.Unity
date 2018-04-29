using Starship.Unity.Events.Models;
using UnityEngine.EventSystems;

namespace Starship.Unity.Events.Combat {
    public interface CanAvoidDamage : IEventSystemHandler {
        void AvoidDamage(Damage damage);
    }
}