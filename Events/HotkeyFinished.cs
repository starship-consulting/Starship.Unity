using System;
using Assets.Scripts.Controls;
using Assets.Scripts.Enumerations;

namespace Assets.Scripts.Events {
    public class HotkeyFinished {
        public HotkeyFinished() {
        }

        public HotkeyFinished(HotkeyTypes hotkey) {
            Hotkey = hotkey;
        }

        public HotkeyTypes Hotkey { get; set; }
    }
}