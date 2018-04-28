using Assets.Scripts.Events.Models;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Events.Combat {
    public interface CanMitigateDamage : IEventSystemHandler {
        void MitigateDamage(Damage damage);
    }
}