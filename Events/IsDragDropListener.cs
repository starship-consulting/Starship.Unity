using Assets.Scripts.Events.Models;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Events {
    public interface IsDragDropListener : IEventSystemHandler {
        void OnDropped(DragResolution drag);
    }
}