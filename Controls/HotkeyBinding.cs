using System;
using UnityEngine;

namespace Starship.Unity.Controls {

    [Serializable]
    public class HotkeyBinding {

        public KeyCode Key;

        public string Name;

        public HotkeyTypes Type;

        //public ActionBinding Action;

        public GameObject HasContext;

        public override string ToString() {
            return Name + " (" + Key + ")";
        }
    }
}