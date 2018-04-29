using Starship.Unity.Core;
using Starship.Unity.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Starship.Unity.UI {
    public class HoverFade : BaseComponent, IPointerEnterHandler, IPointerExitHandler {

        protected override void Start() {
            base.Start();

            if (Target == null) {
                Target = this;
            }

            Target.GetOrAdd<CanvasGroup>().alpha = UnhoveredOpacity;
        }

        public void OnPointerEnter(PointerEventData e) {
            var fade = Target.GetComponent<UIFade>();

            if (fade != null) {
                fade.Destroy();
            }

            fade = Target.AddComponent<UIFade>();
            fade.TargetValue = HoveredOpacity;
            fade.Duration = Duration;
        }

        public void OnPointerExit(PointerEventData e) {
            var fade = Target.GetComponent<UIFade>();

            if (fade != null) {
                fade.Destroy();
            }

            fade = Target.AddComponent<UIFade>();
            fade.TargetValue = UnhoveredOpacity;
            fade.Duration = Duration;
        }

        public MonoBehaviour Target;

        public float HoveredOpacity = 1;

        public float UnhoveredOpacity = 0.8f;

        public float Duration = 0.2f;
    }
}