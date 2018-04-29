using System.Linq;
using Starship.Unity.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Starship.Unity.UI {
    
    public class SegmentedBarPresenter : BaseComponent {

        protected override void OnEnable() {
            base.OnEnable();
            UpdateState();
        }
        
        public void UpdateState() {
            if (BackgroundImage == null) {
                BackgroundImage = GetComponentInChildren<RawImage>();
            }

            if (Repeater == null) {
                Repeater = GetComponentInChildren<Repeater>();
            }

            if (BackgroundImage != null) {
                var color = new Color(Color.r * 0.7f, Color.g * 0.7f, Color.b * 0.7f);

                BackgroundImage.material = new Material(BackgroundImage.material);
                BackgroundImage.material.SetColor("_Color", color);
                BackgroundImage.material.SetColor("_Color2", Color);
            }

            if (Repeater != null) {
                foreach (var element in Repeater.SetCount(MaxValue).Skip(Value)) {
                    var image = element.GetComponent<RawImage>();
                    image.enabled = true;
                }
            }
        }

        public int Value;

        public int MaxValue;

        public Color Color;

        private RawImage BackgroundImage { get; set; }

        private Repeater Repeater { get; set; }
    }
}