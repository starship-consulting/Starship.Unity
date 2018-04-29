using Starship.Unity.Interfaces;
using UnityEngine.EventSystems;

namespace Starship.Unity.EventHandling.Events {
    public class MouseUp : Event, IsSignal {

        public MouseUp() {
        }

        public MouseUp(PointerEventData.InputButton button) {
            Button = button;
        }

        public PointerEventData.InputButton Button { get; set; }
    }
}