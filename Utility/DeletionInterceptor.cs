using Assets.Scripts.Core;
using Assets.Scripts.Interfaces;
using UnityEngine.Events;

namespace Assets.Scripts.Utility {
    public class DeletionInterceptor : BaseComponent, IsDeletable {

        public new void Delete() {
            OnDelete.Invoke();
        }

        public UnityEvent OnDelete;
    }
}