using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Tasks {
    public class Task_Invoke : TaskComponent {
        protected override void OnBegin(MonoBehaviour model) {
        }

        public UnityEvent Invoker;
    }
}