using System;
using Assets.Scripts.Entities;

namespace Assets.Scripts.Events {
    public class ItemDropped {
        public ItemDropped() {
        }

        public ItemDropped(Item item) {
            Item = item;
        }

        public Item Item { get; set; }
    }
}