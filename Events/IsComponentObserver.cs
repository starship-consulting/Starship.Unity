using Assets.Scripts.Events.Models;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Events {
    public interface IsComponentObserver : IEventSystemHandler {
        void OnComponentStateChanged(ComponentStateChanged e);
    }
}