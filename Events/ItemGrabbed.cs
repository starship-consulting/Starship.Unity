using Starship.Unity.Entities;

namespace Starship.Unity.Events {
    public class ItemGrabbed {
        public ItemGrabbed() {
        }

        public ItemGrabbed(Item item) {
            Item = item;
        }

        public Item Item { get; set; }
    }
}