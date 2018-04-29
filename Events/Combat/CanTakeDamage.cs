using Starship.Unity.Events.Models;
using UnityEngine.EventSystems;

namespace Starship.Unity.Events.Combat {
    public interface CanTakeDamage : IEventSystemHandler {
        void TakeDamage(Damage damage);
    }
}