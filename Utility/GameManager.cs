using Assets.Scripts.Core;
using Assets.Scripts.Events;
using UnityEngine;

namespace Assets.Scripts.Utility {
    public class GameManager : BaseComponent {

#if UNITY_EDITOR
        protected void Update() {
        }
#endif

        protected override void OnEnable() {
            base.OnEnable();

            On<PauseStateChanged>(OnPaused);
        }

        private void OnPaused(PauseStateChanged e) {
            Time.timeScale = e.IsPaused ? 0 : 1;
            IsPaused = AudioListener.pause = e.IsPaused;
        }

        public void Quit() {
            Application.Quit();
        }
        
        public bool IsPaused;
    }
}