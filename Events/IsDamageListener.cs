using Starship.Unity.Events.Models;
using UnityEngine.EventSystems;

namespace Starship.Unity.Events {
    public interface IsDamageListener : IEventSystemHandler {
        void OnDamage(Damage damage);
    }
}