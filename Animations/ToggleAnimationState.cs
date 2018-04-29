using Starship.Unity.Core;
using Starship.Unity.Extensions;

namespace Starship.Unity.Animations
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
