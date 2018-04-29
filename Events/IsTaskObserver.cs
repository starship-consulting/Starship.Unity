using UnityEngine.EventSystems;

namespace Starship.Unity.Events {
    public interface IsTaskObserver : IEventSystemHandler {
        void OnAllTasksFinished();
    }
}