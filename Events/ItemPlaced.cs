using System;
using Assets.Scripts.Entities;
using Assets.Scripts.UI;

namespace Assets.Scripts.Events {
    public class ItemPlaced {
        public ItemPlaced() {
        }

        public ItemPlaced(Item item, ItemSlotView slot) {
            Item = item;
            Slot = slot;
        }

        public Item Item { get; set; }

        public ItemSlotView Slot { get; set; }
    }
}