using System.Linq;
using Starship.Unity.Core;
using Starship.Unity.Extensions;

namespace Starship.Unity.Effects {
    public class EffectsController : BaseComponent {

        protected override void Start() {
            base.Start();
            
            Effects = GetComponentsInChildren<Effect>().ToArray();
        }

        public void AddEffect(Effect effect) {
            this.Create(effect);
        }

        public Effect[] Effects;
    }
}