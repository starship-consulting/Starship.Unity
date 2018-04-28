using Assets.Scripts.Core;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.Animations {
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