using Assets.Scripts.Events.Models;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Events.Combat {
    public interface CanTakeDamage : IEventSystemHandler {
        void TakeDamage(Damage damage);
    }
}