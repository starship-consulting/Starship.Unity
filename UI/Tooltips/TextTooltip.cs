using Starship.Unity.Core;
using Starship.Unity.Events;
using UnityEngine.EventSystems;

namespace Starship.Unity.UI.Tooltips {
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