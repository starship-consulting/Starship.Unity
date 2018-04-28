using UnityEngine;

namespace Assets.Scripts.Animations {
    public sealed class WaitForAnimation : CustomYieldInstruction {
        private readonly Animator animator;
        private float innerTime = -1;
        private readonly string lastClipName;

        /// <summary>
        ///     Creates a yield instruction to wait until currently triggered clip is played.
        /// </summary>
        /// <param name="animator"></param>
        public WaitForAnimation(Animator animator) {
            this.animator = animator;
            lastClipName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        }

        public override bool keepWaiting {
            get {
                if (animator.IsInTransition(0)) {
                    return true;
                }

                if (string.CompareOrdinal(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name, lastClipName) != 0) {
                    if (innerTime == -1)
                        innerTime = Time.realtimeSinceStartup + animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

                    if (WaitedFor(innerTime))
                        return false;
                }

                return true;
            }
        }

        private bool WaitedFor(float time) {
            return Time.realtimeSinceStartup >= time;
        }
    }
}