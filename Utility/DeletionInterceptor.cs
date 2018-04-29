using Starship.Unity.Core;
using Starship.Unity.Interfaces;
using UnityEngine.Events;

namespace Starship.Unity.Utility {
    public class DeletionInterceptor : BaseComponent, IsDeletable {

        public new void Delete() {
            OnDelete.Invoke();
        }

        public UnityEvent OnDelete;
    }
}