using System;

namespace Starship.Unity.Utilities {
    public class Disposer : IDisposable {

        public Disposer(Action startAction, Action endAction) {
            EndAction = endAction;
            startAction.Invoke();
        }

        public void Dispose() {
            EndAction.Invoke();
        }

        public Action EndAction { get; set; }
    }
}
