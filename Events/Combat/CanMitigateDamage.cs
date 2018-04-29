using Starship.Unity.Events.Models;
using UnityEngine.EventSystems;

namespace Starship.Unity.Events.Combat {
    public interface CanMitigateDamage : IEventSystemHandler {
        void MitigateDamage(Damage damage);
    }
}