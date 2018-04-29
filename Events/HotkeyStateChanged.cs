using Starship.Unity.Controls;

namespace Starship.Unity.Events {
    public struct HotkeyStateChanged {
        public HotkeyTypes Hotkey;

        public bool IsUp;
    }
}