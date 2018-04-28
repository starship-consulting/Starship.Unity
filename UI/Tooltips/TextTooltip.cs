using Assets.Scripts.Core;
using Assets.Scripts.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.Tooltips {
    public class TextTooltip : BaseComponent, IPointerEnterHandler, IPointerExitHandler {

        public void OnPointerEnter(PointerEventData e) {
            Publish(new TooltipChanged {
                Title = Text
            });
        }

        public void OnPointerExit(PointerEventData e) {
            Publish(new TooltipChanged());
        }

        public string Text;
    }
}