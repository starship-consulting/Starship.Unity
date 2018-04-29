using Starship.Unity.Core;
using Starship.Unity.Extensions;
using UnityEngine;

namespace Starship.Unity.Animations {
    public class TriggerAnimation : BaseComponent {

        protected override void Start() {
            base.Start();
            
            AnimationController = this.FindInParents<MecanimAnimationController>();

            var state = State.GetRandom();

            Debug.Log("Playing animation: " + state);

            if (!AnimationController.IsTriggered(state)) {
                AnimationController.Trigger(state);
            }
            
            //gameObject.Delete();
        }
        
        public string[] State;
        
        private MecanimAnimationController AnimationController;
    }
}