using System;
using Assets.Scripts.Core;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.Animations
{
    public class ToggleAnimationState : BaseComponent {

        protected override void Start() {
            base.Start();

            AnimationController = this.FindInParents<MecanimAnimationController>();
            
            if (DestroyAfterAnimation) {
                AnimationController.SetState(AnimationFlag, Value, OnAnimationFinished);
            }
            else {
                AnimationController.SetState(AnimationFlag, Value);
            }
        }

        protected override void OnStopped() {
            base.OnStopped();
            AnimationController.SetState(AnimationFlag, !Value);
        }

        private void OnAnimationFinished() {
            gameObject.Delete();
        }
        
        public string AnimationFlag;
        
        public bool Value;

        public bool DestroyAfterAnimation;

        private MecanimAnimationController AnimationController;
    }
}
