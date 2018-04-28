using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Utilities {
    public class TypeRouter {
        public TypeRouter() {
            Callbacks = new Dictionary<Type, Dictionary<Guid, MulticastDelegate>>();
            CallbackTypes = new Dictionary<Guid, Type>();
        }

        public Guid On<T>(Action<T> callback) {
            lock (Callbacks) {
                if (!Callbacks.ContainsKey(typeof(T))) {
                    Callbacks.Add(typeof(T), new Dictionary<Guid, MulticastDelegate>());
                }

                var id = Guid.NewGuid();
                Callbacks[typeof(T)].Add(id, callback);

                lock (CallbackTypes) {
                    CallbackTypes.Add(id, typeof(T));
                }

                return id;
            }
        }

        public void Off(Guid id) {
            lock (CallbackTypes) {
                if (CallbackTypes.ContainsKey(id)) {
                    var type = CallbackTypes[id];

                    lock (Callbacks) {
                        Callbacks[type].Remove(id);
                    }

                    CallbackTypes.Remove(id);
                }
            }
        }
        
        public void Publish(object obj) {
            lock (Callbacks) {
                var type = obj.GetType();

                if (Callbacks.ContainsKey(type)) {
                    var callbacks = Callbacks[type].ToList();

                    foreach (var callback in callbacks) {
                        callback.Value.DynamicInvoke(obj);
                    }
                }
            }
        }

        public void Clear() {
            lock (CallbackTypes) {
                CallbackTypes.Clear();
                Callbacks.Clear();
            }
        }

        private Dictionary<Type, Dictionary<Guid, MulticastDelegate>> Callbacks { get; set; }

        private Dictionary<Guid, Type> CallbackTypes { get; set; }
    }
}