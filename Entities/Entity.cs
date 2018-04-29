using Starship.Unity.Core;
using Starship.Unity.Extensions;

namespace Starship.Unity.Entities {
    public class Entity : BaseComponent {
        
        protected override void Awake() {
            foreach(var flag in Template.Flags) {
                foreach(var component in flag.Components) {
                    gameObject.CopyComponentsFrom(component.gameObject);
                }
            }
        }

        public Template Template;
    }
}