using Assets.Scripts.Core;
using Assets.Scripts.Elements;
using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interaction {
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