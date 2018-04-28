using System;
using Assets.Scripts.UI;

namespace Assets.Scripts.Events {
    public class ItemSlotClicked {

        public ItemSlotClicked() {
        }

        public ItemSlotClicked(ItemSlotView slot) {
            Slot = slot;
        }

        public ItemSlotView Slot { get; set; }
    }
}