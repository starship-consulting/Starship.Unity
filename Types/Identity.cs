using Starship.Unity.Core;
using Starship.Unity.Extensions;

namespace Starship.Unity.Types {
    public class Identity : BaseComponent {
        
        protected override void Awake() {
            foreach(var flag in Flags) {
                foreach(var component in flag.Components) {
                    gameObject.CopyComponentsFrom(component.gameObject);
                }
            }
        }

        public string Name;
        
        public GameType[] Flags;
    }
}