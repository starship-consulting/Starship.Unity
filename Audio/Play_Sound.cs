using Starship.Unity.Commands;
using Starship.Unity.Core;

namespace Starship.Unity.Audio {
    public class Play_Sound : BaseComponent {

        protected override void Start() {
            base.Start();

            Publish(new PlaySound(Sound, transform.position));
        }

        public Sound Sound;
    }
}