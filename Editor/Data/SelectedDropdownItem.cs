using System;

namespace Starship.Unity.Editor.Data {
    [Serializable]
    public struct SelectedDropdownItem {
        public int Index;

        public string Key;

        public string Value;
    }
}