using Starship.Unity.Models;

namespace Starship.Unity.Animations {
    public interface IAnimationController {
        void Play(AnimationDefinition definition);
        void Stop();
    }
}