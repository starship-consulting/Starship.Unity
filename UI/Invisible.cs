using Assets.Scripts.Core;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.UI {
    public class Invisible : BaseComponent {

        protected override void Awake() {
            base.Awake();
            CanvasGroup = this.GetOrAdd<CanvasGroup>();
            IsUI = GetComponent<RectTransform>() != null;
        }

        protected override void Start() {
            base.Start();
            Hide();
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            if (IsUI) {
                if (CanvasGroup != null) {
                    CanvasGroup.alpha = 1;
                    CanvasGroup.interactable = true;
                    CanvasGroup.blocksRaycasts = true;
                }
            }
        }
        
        private void Hide() {
            if (IsUI) {
                CanvasGroup.alpha = 0;
                CanvasGroup.interactable = false;
                CanvasGroup.blocksRaycasts = false;
            }
        }

        private bool IsUI { get; set; }

        private CanvasGroup CanvasGroup { get; set; }
    }
}