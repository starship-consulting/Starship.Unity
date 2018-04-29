using Starship.Unity.Events.Models;
using UnityEngine.EventSystems;

namespace Starship.Unity.Events {
    public interface IsDragDropListener : IEventSystemHandler {
        void OnDropped(DragResolution drag);
    }
}