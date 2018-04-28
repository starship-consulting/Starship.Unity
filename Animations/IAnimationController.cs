using Assets.Scripts.Models;

namespace Assets.Scripts.Animations {
    public interface IAnimationController {
        void Play(AnimationDefinition definition);
        void Stop();
    }
}