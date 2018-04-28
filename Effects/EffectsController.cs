using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.Effects {
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