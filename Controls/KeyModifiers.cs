using System;

namespace Assets.Scripts.Controls {

    [Flags]
    public enum KeyModifiers : uint {
        None = 0,
        Shift = 0x1,
        Control = 0x2,
        Alt = 0x4,
        CommandWin = 0x8,
        Numeric = 0x10,
        CapsLock = 0x20,
        FunctionKey = 0x40
    }
}