using Starship.Unity.Core;

namespace Starship.Unity.EventHandling {
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