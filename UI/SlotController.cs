using UnityEngine;
using UnityEngine.UI;

namespace Starship.Unity.UI {
    public class SlotController : MonoBehaviour {

        public void SetLabel(string text) {
            Label.text = text;
        }

        public Text Label;
    }
}