using UnityEngine;
using UnityEngine.Events;

namespace Starship.Unity.Tasks {
    public class Task_Invoke : TaskComponent {
        protected override void OnBegin(MonoBehaviour model) {
        }

        public UnityEvent Invoker;
    }
}