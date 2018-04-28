using System;
using Assets.Scripts.Entities;

namespace Assets.Scripts.Events {
    public class ItemGrabbed {
        public ItemGrabbed() {
        }

        public ItemGrabbed(Item item) {
            Item = item;
        }

        public Item Item { get; set; }
    }
}