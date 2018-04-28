using System;

namespace Assets.Scripts.Core {

    [Serializable]
    public class EventSubscriber {
        public EventSubscriber() {
        }

        public EventSubscriber(Type type) {
            Type = new SerializableType(type);
        }

        public SerializableType Type;
    }
}