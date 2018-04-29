using Starship.Unity.Components;
using Starship.Unity.Core;
using Starship.Unity.UI.Tooltips;
using UnityEngine;
using UnityEngine.UI;

namespace Starship.Unity.UI {
    public class PropertyPresenter : BaseComponent {

        public void SetProperty(CharacterProperty property, Sprite icon) {
            if (Label != null) {
                Label.text = property.Type.ToString();
            }

            if (DisplayTooltip != null) {
                DisplayTooltip.Type = property.Type;
            }

            if (Value != null) {
                Value.text = property.Value.ToString();
            }

            if (Icon != null) {
                Icon.sprite = icon;
            }
        }

        public Text Label;

        public DisplayTooltip DisplayTooltip;

        public Text Value;

        public Image Icon;
    }
}