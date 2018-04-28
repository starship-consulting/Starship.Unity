using System;
using System.Collections.Generic;

namespace Assets.Scripts.Core {
    public class EventContainer {

        public EventContainer() {
            Delegates = new Dictionary<EventToken, MulticastDelegate>();
        }

        public void On(EventToken token, MulticastDelegate callback) {
            Delegates[token] = callback;
        }

        public void Off(EventToken registrar) {
            if (Delegates.ContainsKey(registrar)) {
                Delegates.Remove(registrar);
            }
        }

        public void Invoke(object entity) {
            foreach (var callback in Delegates.Values) {
                callback.DynamicInvoke(entity);
            }
        }

        private Dictionary<EventToken, MulticastDelegate> Delegates { get; set; }
    }
}