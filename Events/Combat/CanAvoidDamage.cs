using Assets.Scripts.Events.Models;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Events.Combat {
    public interface CanAvoidDamage : IEventSystemHandler {
        void AvoidDamage(Damage damage);
    }
}