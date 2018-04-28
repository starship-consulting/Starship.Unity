using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI {
    public class SlotController : MonoBehaviour {

        public void SetLabel(string text) {
            Label.text = text;
        }

        public Text Label;
    }
}