using Starship.Unity.UI;

namespace Starship.Unity.Events {
    public class ItemSlotClicked {

        public ItemSlotClicked() {
        }

        public ItemSlotClicked(ItemSlotView slot) {
            Slot = slot;
        }

        public ItemSlotView Slot { get; set; }
    }
}