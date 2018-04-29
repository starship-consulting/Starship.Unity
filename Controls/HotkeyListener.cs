using Starship.Unity.Core;
using Starship.Unity.Events;
using UnityEngine.Events;

namespace Starship.Unity.Controls {
    public class HotkeyListener : BaseComponent {

        protected override void OnEnable() {
            base.OnEnable();

            On<HotkeyTriggered>(OnHotkeyTriggered);
        }

        private void OnHotkeyTriggered(HotkeyTriggered e) {
            if (e.Hotkey == Hotkey) {
                OnPressed.Invoke();
            }
        }

        public HotkeyTypes Hotkey;

        public UnityEvent OnPressed;
    }
}