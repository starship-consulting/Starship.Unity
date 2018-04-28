using System;
using Assets.Scripts.Controls;

namespace Assets.Scripts.Events {
    public class HotkeyTriggered {
        public HotkeyTriggered() {
        }

        public HotkeyTriggered(HotkeyTypes hotkey) {
            Hotkey = hotkey;
        }

        public HotkeyTypes Hotkey { get; set; }
    }
}