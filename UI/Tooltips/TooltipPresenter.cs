using Assets.Scripts.Core;
using Assets.Scripts.Definitions;
using Assets.Scripts.Enumerations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Tooltips {
    public class TooltipPresenter : BaseComponent {

        protected override void OnEnable() {
            base.OnEnable();

            DisplayTooltip.Enter += OnEnter;
            //DisplayTooltip.Exit += UITooltip.Hide; // Requires reference to UITooltip plugin
        }
        
        private void OnEnter(TooltipModel model) {

            if (model.Type != PropertyTypes.Unknown) {
                var property = Properties.Get(model.Type);

                Icon.sprite = property.Icon;
                Title.text = property.Title;
                Description.text = property.Description;
            }
            else {
                Icon.sprite = model.Icon;
                Title.text = model.Title;
                Description.text = model.Description;
            }

            Icon.gameObject.SetActive(Icon.sprite);

            //UITooltip.AnchorToRect(model.Target.GetComponent<RectTransform>()); // Requires reference to UITooltip plugin
            //UITooltip.Show(); // Requires reference to UITooltip plugin
        }

        public Image Icon;

        public Text Title;

        public Text Description;

        public PropertyManager Properties;
    }
}