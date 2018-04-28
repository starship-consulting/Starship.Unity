using System;
using Assets.Scripts.Events.Models;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Events {
    public interface IsDamageListener : IEventSystemHandler {
        void OnDamage(Damage damage);
    }
}