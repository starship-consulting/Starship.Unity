using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Tasks {
    public class Task_ToggleComponent : TaskComponent {
        
        protected override void OnBegin(MonoBehaviour model) {

            var toggle = model.GetComponent<IsToggleable>();

            if (toggle != null) {
                toggle.Toggle();
            }

            Finish();
        }
    }
}