using Assets.Scripts.Core;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.Types {
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