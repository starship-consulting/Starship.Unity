using Assets.Scripts.Controls;
using Assets.Scripts.Core;
using Assets.Scripts.Events;
using Assets.Scripts.Extensions;
using Assets.Scripts.Interaction;
using Assets.Scripts.Movement;
using UnityEngine;

namespace Assets.Scripts.UI {
    public class WindowController : BaseComponent {
        
        protected override void Start() {
            base.Start();

            CanvasGroup = this.GetOrAdd<CanvasGroup>();
            UpdateState();

            if (OpenSound != null) {
                OpenSound.ignoreListenerPause = true;
            }

            if (CloseSound != null) {
                CloseSound.ignoreListenerPause = true;
            }
        }
        
        public void Toggle() {
            IsOpen = !IsOpen;

            if (IsOpen) {
                if (OpenSound != null) {
                    OpenSound.Play();
                }
            }
            else {
                if (CloseSound != null) {
                    CloseSound.Play();
                }
            }

            if (PauseWhileOpen) {
                Publish(new PauseStateChanged { IsPaused = IsOpen });
            }

            if(UnlockCursorWhileOpen) {
                if(IsOpen) {
                    MouseController.FreeCursor();
                }
                else {
                    MouseController.LockCursor();
                }
            }

            UpdateState();
        }

        private void UpdateState() {
            CanvasGroup.alpha = IsOpen ? 1 : 0;
            CanvasGroup.blocksRaycasts = CanvasGroup.interactable = IsOpen;
        }
        
        public AudioSource OpenSound;

        public AudioSource CloseSound;

        public bool PauseWhileOpen;

        public bool UnlockCursorWhileOpen;

        private bool IsOpen { get; set; }

        private CanvasGroup CanvasGroup { get; set; }
    }
}