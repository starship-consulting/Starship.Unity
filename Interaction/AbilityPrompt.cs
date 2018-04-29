using Starship.Unity.Core;
using Starship.Unity.Elements;
using Starship.Unity.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Starship.Unity.Interaction {
    public class AbilityPrompt : BaseComponent {

        protected override void Start() {
            base.Start();

            Image.sprite = Ability.Icon;
            Tooltip.text = Ability.Title;
        }

        public void Activate() {
            var ability = Parent.Create(Ability);
            //ability.SetTarget(Target);
        }

        public Element Ability;

        public Image Image;

        public Text Tooltip;

        public MonoBehaviour Target;

        public MonoBehaviour Parent;
    }
}