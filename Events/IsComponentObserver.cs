using Starship.Unity.Events.Models;
using UnityEngine.EventSystems;

namespace Starship.Unity.Events {
    public interface IsComponentObserver : IEventSystemHandler {
        void OnComponentStateChanged(ComponentStateChanged e);
    }
}