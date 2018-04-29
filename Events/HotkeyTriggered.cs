using Starship.Unity.Controls;

namespace Starship.Unity.Events {
    public class HotkeyTriggered {
        public HotkeyTriggered() {
        }

        public HotkeyTriggered(HotkeyTypes hotkey) {
            Hotkey = hotkey;
        }

        public HotkeyTypes Hotkey { get; set; }
    }
}