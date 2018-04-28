using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.UI {
    public class InventoryView : MonoBehaviour {

        private void Start() {
            for (var slot = 0; slot < Slots; slot++) {
                this.Create(ItemSlotTemplate, "Slot" + slot);
            }
        }

        public ItemSlotView ItemSlotTemplate;

        public int Slots = 16;
    }
}