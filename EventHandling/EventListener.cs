using System;
using Assets.Scripts.Core;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.EventHandling {
    public class EventListener : BaseComponent {

        protected override void OnEnable() {
            base.OnEnable();

            On<GameEventFired>(OnGameEventFired);
        }

        private void OnGameEventFired(GameEventFired e) {

            // Limit occurences of each triggered event
            foreach (var each in GetComponentsInChildren<TriggeredEvent>()) {
                if (each.Type == e.Event) {
                    return;
                }
            }

            foreach(var mapping in Mappings) {
                if(mapping.Type == e.Event) {
                    var instance = this.Create(mapping.Action);
                    instance.GetOrAdd<TriggeredEvent>().Type = mapping.Type;
                }
            }
        }
        
        public EventMapping[] Mappings;
    }
}