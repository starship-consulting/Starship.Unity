using Starship.Unity.Core;
using Starship.Unity.Entities;
using Starship.Unity.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Starship.Unity.UI {
    public class ItemSlotView : BaseComponent, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {
        
        public void SetItem(Item item) {
            if (item != null) {
                Item = item;
                Icon.sprite = item.Icon;
                Icon.gameObject.SetActive(true);
            }
            else {
                Item = null;
                Icon.sprite = null;
                Icon.gameObject.SetActive(false);
            }
        }

        public void OnPointerDown(PointerEventData eventData) {
            if (Item != null) {
                Publish(new ItemGrabbed(Item));
                SetItem(null);
            }
            else {
                Publish(new ItemSlotClicked(this));
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
            Publish(new UISelected());
        }

        public void OnPointerExit(PointerEventData eventData) {
            Publish(new UIUnselected());
        }

        public Image Icon;

        public Item Item;
    }
}