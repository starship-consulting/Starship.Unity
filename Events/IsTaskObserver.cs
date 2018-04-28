using UnityEngine.EventSystems;

namespace Assets.Scripts.Events {
    public interface IsTaskObserver : IEventSystemHandler {
        void OnAllTasksFinished();
    }
}