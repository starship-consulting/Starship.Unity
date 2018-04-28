using Assets.Scripts.Commands;
using Assets.Scripts.Core;

namespace Assets.Scripts.Audio {
    public class Play_Sound : BaseComponent {

        protected override void Start() {
            base.Start();

            Publish(new PlaySound(Sound, transform.position));
        }

        public Sound Sound;
    }
}