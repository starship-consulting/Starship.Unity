using Assets.Scripts.Controls;

namespace Assets.Scripts.Events {
    public struct HotkeyStateChanged {
        public HotkeyTypes Hotkey;

        public bool IsUp;
    }
}