using UnityEngine;

namespace Assets.Scripts.Components {
    public class SelectableItem : MonoBehaviour {

        public void Select() {
            gameObject.SetActive(true);

            foreach (Transform child in transform.parent) {
                if (child.name != name) {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }
}