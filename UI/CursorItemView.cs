using Starship.Unity.Core;
using Starship.Unity.Entities;
using Starship.Unity.EventHandling.Events;
using Starship.Unity.Events;
using Starship.Unity.Extensions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Starship.Unity.UI {
    public class CursorItemView : BaseComponent {

        protected override void Awake() {
            base.Awake();
            Image = this.GetOrAdd<Image>();

            On<MouseDown>(Clicked);
            On<ItemGrabbed>(OnItemGrabbed);
            On<ItemSlotClicked>(OnItemSlotClicked);
            On<UIUnselected>(OnUIUnselected);
            On<UISelected>(OnUISelected);
        }

        private void OnUIUnselected(UIUnselected e) {
            IsUISelected = false;
        }

        private void OnUISelected(UISelected e) {
            IsUISelected = true;
        }

        private void Clicked(MouseDown e) {
            if (IsUISelected) {
                return;
            }

            if (CursorItem != null && e.Button == PointerEventData.InputButton.Left) {
                Throw();
            }
        }

        public void Throw() {
            var item = CursorItem.GetComponent<Item>();
            item.Drop();
            ClearItem();
        }

        private void OnItemSlotClicked(ItemSlotClicked e) {
            if (CursorItem != null) {
                e.Slot.SetItem(CursorItem);
                ClearItem();
            }
        }

        private void OnItemGrabbed(ItemGrabbed e) {
            CursorItem = e.Item;
            Image.sprite = e.Item.Icon;
            Image.enabled = true;
        }

        private void ClearItem() {
            Image.sprite = null;
            Image.enabled = false;
            CursorItem = null;
        }

        public Item CursorItem;

        public bool IsUISelected;

        private Image Image { get; set; }
    }
}