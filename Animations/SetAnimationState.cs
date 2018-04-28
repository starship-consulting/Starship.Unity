using Assets.Scripts.Core;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.Animations {
    public class SetAnimationState : BaseComponent {

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
            AnimationController.SetState(AnimationFlag, 0);
        }

        private void OnAnimationFinished() {
            gameObject.Delete();
        }
        
        public string AnimationFlag;
        
        public float Value;

        public bool DestroyAfterAnimation;

        private MecanimAnimationController AnimationController;
    }
}