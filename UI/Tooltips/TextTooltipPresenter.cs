using Assets.Scripts.Core;
using Assets.Scripts.Events;
using Assets.Scripts.Events.Targetting;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Tooltips {
    public class TextTooltipPresenter : BaseComponent {

        protected override void Awake() {
            base.Awake();
            Text = GetComponent<Text>();
        }

        protected override void OnEnable() {
            base.OnEnable();

            On<TargetChanged>(OnTargetChanged);
            On<TooltipChanged>(OnTooltipChanged);
            Label.LabelChanged += OnSelectableItemChanged;
        }

        private void OnTooltipChanged(TooltipChanged tooltip) {
            Text.text = tooltip.Title;
        }

        private void OnSelectableItemChanged(string text) {
            Text.text = text;
        }

        private void OnTargetChanged(TargetChanged e) {
            Text.text = e.Target != null ? e.Target.Tooltip : string.Empty;
        }

        private Text Text { get; set; }
    }
}