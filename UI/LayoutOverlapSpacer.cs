using Assets.Scripts.Core;
using UnityEngine.UI;

namespace Assets.Scripts.UI {
    public class LayoutOverlapSpacer : BaseComponent {

        protected override void Awake() {
            base.Awake();
            LayoutGroup = GetComponent<HorizontalLayoutGroup>();
        }

        private HorizontalLayoutGroup LayoutGroup { get; set; }
    }
}