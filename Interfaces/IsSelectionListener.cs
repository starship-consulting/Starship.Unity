using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Interfaces {
    public interface IsSelectionListener : IEventSystemHandler {
        void OnItemSelected(MonoBehaviour source);
    }
}