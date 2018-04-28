using Assets.Scripts.Core;
using Assets.Scripts.Events;
using UnityEngine.Events;

namespace Assets.Scripts.Controls {
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