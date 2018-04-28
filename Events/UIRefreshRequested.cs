using System;
using Assets.Scripts.ScriptableObjects;

namespace Assets.Scripts.Events {

    [Serializable]
    public class UIRefreshRequested {
        public UIRefreshRequested() {
        }

        public UIRefreshRequested(DataObject source) {
            Source = source;
        }

        public DataObject Source;
    }
}