using UnityEngine;
using UnityEngine.EventSystems;

namespace Starship.Unity.Interfaces {
    public interface IsSelectionListener : IEventSystemHandler {
        void OnItemSelected(MonoBehaviour source);
    }
}