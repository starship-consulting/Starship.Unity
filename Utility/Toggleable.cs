using UnityEngine;

namespace Assets.Scripts.Utility {
    public class Toggleable : MonoBehaviour {

        private void Start() {
            if (!StartActive) {
                ToggleOff();
            }
        }

        public void Toggle() {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        public void ToggleOff() {
            gameObject.SetActive(false);
        }

        public bool StartActive = true;
    }
}