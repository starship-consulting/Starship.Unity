using Starship.Unity.Controls;

namespace Starship.Unity.Events {
    public class HotkeyFinished {
        public HotkeyFinished() {
        }

        public HotkeyFinished(HotkeyTypes hotkey) {
            Hotkey = hotkey;
        }

        public HotkeyTypes Hotkey { get; set; }
    }
}