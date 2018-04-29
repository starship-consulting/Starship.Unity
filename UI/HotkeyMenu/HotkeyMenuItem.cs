using System;
using UnityEngine;

namespace Starship.Unity.UI.HotkeyMenu {

    [Serializable]
    public struct HotkeyMenuItem {

        public string Text;
    
        public Sprite Icon;

        public int Width;

        public int Height;

        //public ActionBinding Clicked;
    }
}