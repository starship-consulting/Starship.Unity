using System;
using Starship.Unity.ScriptableObjects;

namespace Starship.Unity.Events {

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