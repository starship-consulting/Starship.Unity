using Starship.Unity.Entities;

namespace Starship.Unity.Events {
    public class ItemDropped {
        public ItemDropped() {
        }

        public ItemDropped(Item item) {
            Item = item;
        }

        public Item Item { get; set; }
    }
}