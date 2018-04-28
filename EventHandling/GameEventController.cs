using System;
using Assets.Scripts.Core;

namespace Assets.Scripts.EventHandling {
    public class GameEventController : BaseComponent {

        protected override void OnEnable() {
            base.OnEnable();

            On<GameEventFired>(OnGameEventFired);
        }

        private void OnGameEventFired(GameEventFired e) {
        }

        public static void RegisterListener() {

        }
    }
}