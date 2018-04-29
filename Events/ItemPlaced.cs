using Starship.Unity.Entities;
using Starship.Unity.UI;

namespace Starship.Unity.Events {
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