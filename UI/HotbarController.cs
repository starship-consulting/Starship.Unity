using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.UI {
    public class HotbarController : MonoBehaviour {

        private void Awake() {
            
        }

        private void Start() {
            for (var count = 1; count <= Slots; count++) {
                var slot = this.Create(SlotTemplate);

                var label = count == 10 ? "0" : count.ToString();
                slot.SetLabel(label);
            }
        }

        public int Slots;

        public SlotController SlotTemplate;
    }
}
