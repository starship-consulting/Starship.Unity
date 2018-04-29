using Starship.Unity.Core;
using UnityEngine.UI;

namespace Starship.Unity.UI {
    public class LayoutOverlapSpacer : BaseComponent {

        protected override void Awake() {
            base.Awake();
            LayoutGroup = GetComponent<HorizontalLayoutGroup>();
        }

        private HorizontalLayoutGroup LayoutGroup { get; set; }
    }
}